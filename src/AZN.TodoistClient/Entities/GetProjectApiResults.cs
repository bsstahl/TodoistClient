using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Results returned by the GetProject API call, including the returned projects
/// and an optional cursor for the next page of results.
/// </summary>
public sealed class GetProjectApiResults
{
    /// <summary>
    /// Projects returned by the API call.
    /// </summary>
    [JsonPropertyName("results")]
    public IEnumerable<Project> Projects { get; set; } = [];

    /// <summary>
    /// Cursor for fetching the next page of results, or null if there are no more pages.
    /// </summary>
    [JsonPropertyName("next_cursor")]
    public string? NextCursor { get; set; }
}

