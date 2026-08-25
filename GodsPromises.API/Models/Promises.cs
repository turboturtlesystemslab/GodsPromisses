namespace GodsPromises.Api.Models;

public class Promise
{
    public int Id { get; set; }

    public string Reference { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public string Theme { get; set; } = string.Empty;

    public string Language { get; set; } = "de";
}