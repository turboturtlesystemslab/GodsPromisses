using System.Text.Json.Serialization;

namespace GodsPromises.Api.Models;

public class BibleBooksResponse
{
    [JsonPropertyName("language")]
    public string Language { get; set; } = "";

    [JsonPropertyName("name")]
    public string Name { get; set; } = "";

    [JsonPropertyName("description")]
    public string Description { get; set; } = "";

    [JsonPropertyName("books")]
    public List<BibleBook> Books { get; set; } = [];
}