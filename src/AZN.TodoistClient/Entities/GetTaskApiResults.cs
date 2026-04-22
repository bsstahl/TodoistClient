using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents the response from the Todoist API for a task list request,
/// containing the returned tasks and a cursor for the next page of results.
/// </summary>
public sealed class GetTaskApiResults
{
    /// <summary>
    /// The collection of tasks returned by the API.
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<Entities.Item> Tasks { get; set; } = [];

    /// <summary>
    /// Cursor for the next page of results, or null/empty if there are no more pages.
    /// </summary>
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}

