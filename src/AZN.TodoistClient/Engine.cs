using System.Text.Json;
using AZN.TodoistClient.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AZN.TodoistClient;

/// <summary>
/// The primary client. Use this object as the foundation for all
/// interactions with the Todoist API
/// </summary>
public class Engine : IDisposable
{
    /// <summary>
    /// The URL used by the client to interact with the Todoist API
    /// </summary>
    public static Uri BaseUrl => new("https://api.todoist.com/api/v1/");

    private readonly HttpClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<Engine> _logger;
    private readonly JsonSerializerOptions _serializerOptions;

    private bool disposedValue;

    #region Constructors

    /// <summary>
    /// The default public constructor for the client
    /// </summary>
    public Engine(ILogger<Engine> logger, IConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(config, nameof(config));

        _logger = logger;
        _config = config;

        _serializerOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
        };

        _client = new HttpClient()
        {
            BaseAddress = Engine.BaseUrl
        };
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.GetApiToken());
    }

    /// <summary>
    /// An internal-only constructor used for testing that accepts
    /// a mock HttpMessageHandler to allow for simulating API responses 
    /// without making actual HTTP calls.
    /// </summary>
    /// <param name="messageHandler">An HttpMessageHandler that returns known responses</param>
    internal Engine(HttpMessageHandler messageHandler)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Public API Methods

    /// <summary>
    /// Retrieves task updates from the Todoist API using the provided sync token.
    /// </summary>
    /// <param name="syncToken">The sync token used to request incremental updates. Use "*" (default) to request a full sync.</param>
    /// <returns>A task that resolves to a GetTaskUpdatesApiResults containing update items and the resulting sync token.</returns>
    public async Task<GetSyncApiResults> GetSyncUpdates(string syncToken = "*")
    {
        const string resourceTypesValue = "[\"items\",\"projects\"]";

        var form = new Dictionary<string, string>
        {
            ["sync_token"] = syncToken,
            ["resource_types"] = resourceTypesValue
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, "sync")
        {
            Content = new FormUrlEncodedContent(form)
        };

        var response = await _client.SendAsync(request).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<GetSyncApiResults>(json);
        return result ?? new GetSyncApiResults() { SyncToken = string.Empty }; 
    }

    /// <summary>
    /// Retrieves all tasks from the Todoist API by paging through results until all results have been retrieved.
    /// </summary>
    /// <returns>A task that resolves to an IEnumerable of Item containing all tasks.</returns>
    public async Task<IEnumerable<Entities.Item>> GetAllTasks()
    {
        var allTasks = new List<Entities.Item>();

        string? nextCursor = null;
        do
        {
            var tasks = await this.GetPageOfTasks(nextCursor).ConfigureAwait(false);
            if (tasks is not null)
                allTasks.AddRange(tasks.Tasks);
            nextCursor = tasks?.NextCursor;
        } while (nextCursor is not null);

        return allTasks;
    }

    /// <summary>
    /// Retrieves all projects from the Todoist API by paging through results until all results have been retrieved.
    /// </summary>
    /// <returns>A task that resolves to an IEnumerable of Project containing all projects.</returns>
    public async Task<IEnumerable<Entities.Project>> GetAllProjects()
    {
        var allProjects = new List<Entities.Project>();

        string? nextCursor = null;
        do
        {
            var projects = await this.GetPageOfProjects(nextCursor).ConfigureAwait(false);
            if (projects is not null)
                allProjects.AddRange(projects.Projects);
            nextCursor = projects?.NextCursor;
        } while (nextCursor is not null);

        return allProjects;
    }

    /// <summary>
    /// Updates a task in the Todoist API using the provided ItemUpdate object. 
    /// The Id property of the ItemUpdate must be set to the ID of the task to update.
    /// </summary>
    /// <param name="itemUpdate">The ItemUpdate object containing the updated task information.</param>
    public async Task UpdateTask(Entities.ItemUpdate itemUpdate)
    {
        ArgumentNullException.ThrowIfNull(itemUpdate, nameof(itemUpdate));
        var jsonContent = JsonSerializer.Serialize(itemUpdate, _serializerOptions);
        var urlPath = $"tasks/{itemUpdate.Id}";
        using var request = new HttpRequestMessage(HttpMethod.Post, urlPath)
        {
            Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json")
        };

#pragma warning disable CA1848 // Use the LoggerMessage delegates
#pragma warning disable CA1873 // Avoid potentially expensive logging
        _logger.LogTrace("Updating task with ID {TaskId} using URL {UrlPath} and payload:\r\n\r\n{Payload}\r\n\r\n",
            itemUpdate.Id, urlPath, jsonContent);
        var response = await _client.SendAsync(request).ConfigureAwait(false);
        _logger.LogTrace("Response received from http request: {Response}", response);
#pragma warning restore CA1873 // Avoid potentially expensive logging
#pragma warning restore CA1848 // Use the LoggerMessage delegates

        response.EnsureSuccessStatusCode();
    }

    #endregion

    #region Private Methods

    private string GetApiToken()
    {
        return _config["TODOIST_API_TOKEN"] 
            ?? throw new InvalidOperationException("API token not found in configuration.");
    }

    private async Task<GetProjectApiResults?> GetPageOfProjects(string? nextCursor)
    {
        var parameterizedUrl = string.IsNullOrWhiteSpace(nextCursor)
            ? $"{Engine.BaseUrl}projects" : $"{Engine.BaseUrl}projects?cursor={nextCursor}";
        var json = await _client.GetStringAsync(new Uri(parameterizedUrl)).ConfigureAwait(false);
        return JsonSerializer.Deserialize<Entities.GetProjectApiResults>(json);
    }

    private async Task<GetTaskApiResults?> GetPageOfTasks(string? nextCursor = null)
    {
        var fullUrl = string.IsNullOrWhiteSpace(nextCursor)
            ? $"{Engine.BaseUrl}tasks" : $"{Engine.BaseUrl}tasks?cursor={nextCursor}";
        var json = await _client.GetStringAsync(new Uri(fullUrl)).ConfigureAwait(false);
        return JsonSerializer.Deserialize<Entities.GetTaskApiResults>(json);
    }

    #endregion

    #region IDisposable Support

    /// <summary>
    /// Releases the resources used by the Engine.
    /// </summary>
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources; false to release only unmanaged resources.
    /// </param>
    protected virtual void Dispose(Boolean disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _client?.Dispose();
            }

            disposedValue = true;
        }
    }

    /// <summary>
    /// Clean up all managed resources
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion
}
