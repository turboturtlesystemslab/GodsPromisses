using System.Text.Json.Serialization;

namespace GodsPromises.Api.Models;

public class BibleRandomResponse
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

public class BibleBook
{
    [JsonPropertyName("book_number")]
    public int Book_Number { get; set; }

    [JsonPropertyName("short_name")]
    public string Short_Name { get; set; } = "";

    [JsonPropertyName("long_name")]
    public string Long_Name { get; set; } = "";

    [JsonPropertyName("chapters")]
    public List<BibleChapter> Chapters { get; set; } = [];
}

public class BibleChapter
{
    // HolyBible API liefert: "chapter": 14
    [JsonPropertyName("chapter")]
    public int Chapter_Number { get; set; }

    [JsonPropertyName("verses")]
    public List<BibleVerse> Verses { get; set; } = [];
}

public class BibleVerse
{
    [JsonPropertyName("verse")]
    public int Verse { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; } = "";
}