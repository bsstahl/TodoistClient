using System.Text.Json;
using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents the results returned by the Todoist API when requesting project updates.
/// Contains information about whether a full sync was performed, the sync timestamp,
/// the list of project updates, and the sync token for subsequent requests.
/// </summary>
public class GetSyncApiResults
{
    /// <summary>
    /// Gets or sets a value indicating whether the API response represents a full sync.
    /// </summary>
    [JsonPropertyName("full_sync")]
    public bool FullSync { get; set; }

    /// <summary>
    /// Gets or sets the UTC date/time when the full sync occurred.
    /// </summary>
    [JsonPropertyName("full_sync_date_utc")]
    public DateTimeOffset FullSyncDateUtc { get; set; }

    /// <summary>
    /// Gets or sets the collection of project updates returned by the API.
    /// </summary>
    [JsonPropertyName("projects")]
    public IReadOnlyCollection<ProjectUpdate> ProjectUpdates { get; set; } = [];

    /// <summary>
    /// Gets or sets the collection of item updates returned by the API.
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyCollection<ItemUpdate> ItemUpdates { get; set; } = [];

    /// <summary>
    /// Gets or sets the sync token to use for the next incremental sync request.
    /// </summary>
    [JsonPropertyName("sync_token")]
    public required string SyncToken { get; set; }

    /// <summary>
    /// Any other elements in the payload not specifically assigned 
    /// to properties in this class will be captured here as a dictionary of JSON elements.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> Other { get; } = [];
}
