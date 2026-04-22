using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents a due date for a Todoist item.
/// </summary>
public class DueDate
{
    /// <summary>
    /// The date and time of the due date.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Timezone of the due date (for example "UTC" or "America/Los_Angeles").
    /// </summary>
    [JsonPropertyName("timezone")]
    public required string Timezone { get; set; }

    /// <summary>
    /// Natural-language description of the due date (JSON field "string").
    /// </summary>
    [JsonPropertyName("string")]
    public string? Description { get; set; }

    /// <summary>
    /// Language code used in the description.
    /// </summary>
    [JsonPropertyName("lang")]
    public required string Lang { get; set; }

    /// <summary>
    /// True if the due date is recurring.
    /// </summary>
    [JsonPropertyName("is_recurring")]
    public bool IsRecurring { get; set; }
}
