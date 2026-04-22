using System.Text.Json.Serialization;

namespace AZN.TodoistClient.Entities;

/// <summary>
/// Represents an embedding vector produced by a specific model.
/// </summary>
public class Embedding
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Embedding"/> class.
    /// </summary>
    /// <param name="model">The model identifier that produced the embedding.</param>
    /// <param name="vector">The embedding vector values.</param>
    public Embedding(string model, IReadOnlyCollection<Single> vector)
    {
        this.Model = model;
        this.Vector = vector;
    }

    /// <summary>
    /// Gets or sets the model identifier used to create the embedding.
    /// </summary>
    [JsonPropertyName("model")]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the embedding vector as a list of single-precision floats.
    /// </summary>
    [JsonPropertyName("vector")]
    public IReadOnlyCollection<Single> Vector { get; set; }
}

