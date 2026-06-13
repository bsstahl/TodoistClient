using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents a Todoist item (task) returned by the API.
/// </summary>
public class Item
{
    /// <summary>
    /// The id of the user who owns the item.
    /// </summary>
    [JsonPropertyName("user_id")]
    public required string UserId { get; set; }

    /// <summary>
    /// The unique identifier of the item.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The project id that this item belongs to.
    /// </summary>
    [JsonPropertyName("project_id")]
    public required string ProjectId { get; set; }

    /// <summary>
    /// The section id that this item belongs to.
    /// </summary>
    [JsonPropertyName("section_id")]
    public string? SectionId { get; set; }

    /// <summary>
    /// The parent item id if this item is a sub-item.
    /// </summary>
    [JsonPropertyName("parent_id")]
    public string? ParentId { get; set; }

    /// <summary>
    /// Labels applied to the item.
    /// </summary>
    [JsonPropertyName("labels")]
    public IReadOnlyCollection<string> Labels { get; set; } = [];

    /// <summary>
    /// The item's deadline information.
    /// </summary>
    [JsonPropertyName("deadline")]
    public Entities.Deadline? Deadline { get; set; }

    /// <summary>
    /// Whether the item is checked/completed.
    /// </summary>
    [JsonPropertyName("checked")]
    public bool IsChecked { get; set; }

    /// <summary>
    /// Whether the item has been deleted.
    /// </summary>
    [JsonPropertyName("is_deleted")]
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The date and time when the item was added.
    /// </summary>
    [JsonPropertyName("added_at")]
    public DateTimeOffset? AddedAt { get; set; }

    /// <summary>
    /// The date and time when the item was completed.
    /// </summary>
    [JsonPropertyName("completed_at")]
    public DateTimeOffset? CompletedAt { get; set; }

    /// <summary>
    /// The id of the user who completed the item.
    /// </summary>
    [JsonPropertyName("completed_by_uid")]
    public string? CompletedById { get; set; }

    /// <summary>
    /// The last update timestamp for the item.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }

    /// <summary>
    /// Due date information for the item.
    /// </summary>
    [JsonPropertyName("due")]
    public Entities.DueDate? Due { get; set; }

    /// <summary>
    /// The item's priority value.
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; set; }

    /// <summary>
    /// The child order index of the item within its parent/project.
    /// </summary>
    [JsonPropertyName("child_order")]
    public int ChildOrder { get; set; }

    /// <summary>
    /// The item's main content (title).
    /// </summary>
    [JsonPropertyName("content")]
    public required string Content { get; set; }

    /// <summary>
    /// The item's full description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// A stored embedding for the item (if used by the application).
    /// </summary>
    public Embedding? Embedding { get; set; }

    /// <summary>
    /// Indicates whether the item is a recurring task (i.e. has a due date that is recurring).
    /// </summary>
    public bool IsRecurring => this.Due?.IsRecurring ?? false;

    private static IEnumerable<string> DailyRecurringPatterns => new List<string>()
    {
        "every day",
        "every! day",
        "every 1 day",
        "every! 1 day"
    };

    /// <summary>
    /// Indicates whether the item is a daily recurring task.
    /// </summary>
    public bool IsDailyRecurring => 
        this.IsRecurring 
        && Item.DailyRecurringPatterns.Contains(this.Due!.Description);
}
