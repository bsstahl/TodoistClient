using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents a duration with an amount and unit (e.g. 5 days).
/// </summary>
public class Duration
{
    /// <summary>
    /// The numeric amount of the duration.
    /// </summary>
    [JsonPropertyName("amount")]
    public required int Amount { get; set; }

    /// <summary>
    /// The unit of the duration (for example "day", "hour", "minute").
    /// </summary>
    [JsonPropertyName("unit")]
    public required string Unit { get; set; }
}
