using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents a Todoist project.
/// </summary>
public class Project
{
    /// <summary>
    /// The project's identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The project's name.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// The project's color.
    /// </summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>
    /// Identifier of the parent project, if any.
    /// </summary>
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    /// <summary>
    /// The project's order/index.
    /// </summary>
    [JsonPropertyName("order")]
    public int Order { get; set; }

    /// <summary>
    /// Whether the project has been archived.
    /// </summary>
    [JsonPropertyName("is_archived")]
    public bool IsArchived { get; set; }

    /// <summary>
    /// Whether the project has been deleted.
    /// </summary>
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Whether the project is shared.
    /// </summary>
    [JsonPropertyName("is_shared")]
    public bool IsShared { get; set; }

    /// <summary>
    /// Whether the project is marked as favorite.
    /// </summary>
    [JsonPropertyName("is_favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>
    /// Whether this project is the inbox project.
    /// </summary>
    [JsonPropertyName("is_inbox_project")]
    public bool IsInboxProject { get; set; }

    /// <summary>
    /// Whether this project is a team inbox.
    /// </summary>
    [JsonPropertyName("is_team_inbox")]
    public bool IsTeamInbox { get; set; }

    /// <summary>
    /// The project's view style.
    /// </summary>
    [JsonPropertyName("view_style")]
    public required string ViewStyle { get; set; }

    /// <summary>
    /// The project's URL.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; }
}
