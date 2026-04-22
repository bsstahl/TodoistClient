using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents a task deadline including the date and language metadata returned by the API.
/// </summary>
public class Deadline
{
    /// <summary>
    /// The deadline date and time.
    /// </summary>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    /// <summary>
    /// Language code associated with the deadline (for example "en").
    /// </summary>
    [JsonPropertyName("lang")]
    public required string Lang { get; set; }
}

