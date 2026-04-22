using System;
using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents an update to a Todoist project.
/// Contains properties returned by the Todoist API when a project is updated.
/// </summary>
public class ProjectUpdate
{
    // The Access property is not needed at this time
    //[JsonPropertyName("access")]
    //public object Access { get; set; }

    /// <summary>Whether tasks can be assigned in the project.</summary>
    [JsonPropertyName("can_assign_tasks")]
    public bool CanAssignTasks { get; set; }

    /// <summary>Whether comments are allowed in the project.</summary>
    [JsonPropertyName("can_comment")]
    public bool CanComment { get; set; }

    /// <summary>Ordering index among child projects.</summary>
    [JsonPropertyName("child_order")]
    public int ChildOrder { get; set; }

    /// <summary>Project color code or name.</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>Timestamp when the project was created.</summary>
    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>User id of the creator.</summary>
    [JsonPropertyName("creator_uid")]
    public required string CreatorUid { get; set; }

    /// <summary>Default ordering value for tasks in the project.</summary>
    [JsonPropertyName("default_order")]
    public int DefaultOrder { get; set; }

    /// <summary>Description text of the project.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Unique identifier of the project.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>Whether this is the inbox project.</summary>
    [JsonPropertyName("inbox_project")]
    public bool InboxProject { get; set; }

    /// <summary>Whether the project is archived.</summary>
    [JsonPropertyName("is_archived")]
    public bool IsArchived { get; set; }

    /// <summary>Whether the project is displayed collapsed.</summary>
    [JsonPropertyName("is_collapsed")]
    public bool IsCollapsed { get; set; }

    /// <summary>Whether the project has been deleted.</summary>
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>Whether the project is marked as a favorite.</summary>
    [JsonPropertyName("is_favorite")]
    public bool IsFavorite { get; set; }

    /// <summary>Whether the project is frozen (no changes allowed).</summary>
    [JsonPropertyName("is_frozen")]
    public bool IsFrozen { get; set; }

    /// <summary>Whether the project is shared with others.</summary>
    [JsonPropertyName("is_shared")]
    public bool IsShared { get; set; }

    /// <summary>Project name.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>Parent project identifier, if any.</summary>
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    /// <summary>Whether the project has public access enabled.</summary>
    [JsonPropertyName("public_access")]
    public bool PublicAccess { get; set; }

    /// <summary>Public key associated with the project.</summary>
    [JsonPropertyName("public_key")]
    public string? PublicKey { get; set; }

    /// <summary>Role of the current user in the project.</summary>
    [JsonPropertyName("role")]
    public string? Role { get; set; }

    /// <summary>Timestamp when the project was last updated.</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>View style for the project (e.g., list, board).</summary>
    [JsonPropertyName("view_style")]
    public required string ViewStyle { get; set; }
}
