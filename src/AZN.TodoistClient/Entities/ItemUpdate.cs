using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents an update record for a Todoist item.
/// </summary>
public class ItemUpdate
{
    /// <summary>Time when the item was added.</summary>
    [JsonPropertyName("added_at")]
    public DateTimeOffset AddedAt { get; set; }

    /// <summary>UID of the user who added the item.</summary>
    [JsonPropertyName("added_by_uid")]
    public required string AddedByUid { get; set; }

    /// <summary>UID of the user who assigned the item (if any).</summary>
    [JsonPropertyName("assigned_by_uid")]
    public string? AssignedByUid { get; set; }

    /// <summary>Whether the item is checked (completed).</summary>
    [JsonPropertyName("checked")]
    public bool Checked { get; set; }

    /// <summary>Child order index within the parent.</summary>
    [JsonPropertyName("child_order")]
    public int ChildOrder { get; set; }

    /// <summary>Time when the item was completed, if completed.</summary>
    [JsonPropertyName("completed_at")]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>UID of the user who completed the item (if any).</summary>
    [JsonPropertyName("completed_by_uid")]
    public string? CompletedByUid { get; set; }

    /// <summary>Text content of the item.</summary>
    [JsonPropertyName("content")]
    public required string Content { get; set; }

    /// <summary>Order within the day view.</summary>
    [JsonPropertyName("day_order")]
    public int DayOrder { get; set; }

    /// <summary>Deadline information for the item.</summary>
    [JsonPropertyName("deadline")]
    public Deadline? Deadline { get; set; }

    /// <summary>Detailed description for the item.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>Due date information for the item.</summary>
    [JsonPropertyName("due")]
    public DueDate? Due { get; set; }

    /// <summary>Duration value associated with the item (implementation-specific).</summary>
    [JsonPropertyName("duration")]
    public Duration? Duration { get; set; }

    /// <summary>List of goal IDs associated with the item.</summary>
    [JsonPropertyName("goal_ids")]
    public IReadOnlyCollection<string> GoalIds { get; set; } = [];

    /// <summary>Item identifier.</summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>Whether the item is collapsed in the UI.</summary>
    [JsonPropertyName("is_collapsed")]
    public bool IsCollapsed { get; set; }

    /// <summary>Whether the item has been deleted.</summary>
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>Label IDs attached to the item.</summary>
    [JsonPropertyName("labels")]
    public IReadOnlyCollection<string> Labels { get; set; } = [];

    /// <summary>Number of notes attached to the item.</summary>
    [JsonPropertyName("note_count")]
    public int NoteCount { get; set; }

    /// <summary>Parent item identifier (if any).</summary>
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    /// <summary>Priority level of the item.</summary>
    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    /// <summary>Identifier of the project containing the item.</summary>
    [JsonPropertyName("project_id")]
    public required string ProjectId { get; set; }

    /// <summary>UID of the responsible user for the item.</summary>
    [JsonPropertyName("responsible_uid")]
    public string? ResponsibleUid { get; set; }

    /// <summary>Section identifier within the project.</summary>
    [JsonPropertyName("section_id")]
    public string? SectionId { get; set; }

    /// <summary>Last update time for the item.</summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; set; }

    /// <summary>UID of the owning user.</summary>
    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }
}
