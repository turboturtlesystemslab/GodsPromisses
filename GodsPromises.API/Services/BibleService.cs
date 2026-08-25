using System.Text.Json;
using GodsPromises.Api.Models;

namespace GodsPromises.Api.Services;

public class BibleService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public BibleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    // =========================================================
    // RANDOM VERSE
    // =========================================================

    public async Task<BibleRandomResponse?> GetRandomVerseAsync(
        string language)
    {
        language = NormalizeLanguage(language);

        var url =
            $"random?language={Uri.EscapeDataString(language)}";

        var response =
            await _httpClient.GetAsync(url);

        var json =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bible API returned {(int)response.StatusCode}: {json}");
        }

        return DeserializeResponse<BibleRandomResponse>(json);
    }


    // =========================================================
    // BOOKS
    // =========================================================

    public async Task<BibleBooksResponse?> GetBooksAsync(
        string language)
    {
        language = NormalizeLanguage(language);

        var url =
            $"books?language={Uri.EscapeDataString(language)}";

        var response =
            await _httpClient.GetAsync(url);

        var json =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bible API returned {(int)response.StatusCode}: {json}");
        }

        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            using var document =
                JsonDocument.Parse(json);

            var root =
                document.RootElement;

            if (root.ValueKind !=
                JsonValueKind.Object)
            {
                return null;
            }

            var result =
                new BibleBooksResponse
                {
                    Language =
                        GetString(
                            root,
                            "language"),

                    Name =
                        GetString(
                            root,
                            "name"),

                    Description =
                        GetString(
                            root,
                            "description")
                };

            if (!root.TryGetProperty(
                    "books",
                    out var booksElement) ||
                booksElement.ValueKind !=
                    JsonValueKind.Array)
            {
                return result;
            }

            foreach (var bookElement
                     in booksElement.EnumerateArray())
            {
                if (bookElement.ValueKind !=
                    JsonValueKind.Object)
                {
                    continue;
                }

                var book =
                    new BibleBook
                    {
                        Book_Number =
                            GetInt(
                                bookElement,
                                "book_number"),

                        Short_Name =
                            GetString(
                                bookElement,
                                "short_name"),

                        Long_Name =
                            GetString(
                                bookElement,
                                "long_name")
                    };

                if (bookElement.TryGetProperty(
                        "chapters",
                        out var chaptersElement) &&
                    chaptersElement.ValueKind ==
                        JsonValueKind.Array)
                {
                    ReadChapters(
                        book,
                        chaptersElement);
                }

                result.Books.Add(book);
            }

            return result;
        }
        catch (JsonException ex)
        {
            throw new JsonException(
                $"Die Bible API lieferte ungültiges JSON für " +
                $"/books?language={language}." +
                Environment.NewLine +
                $"JSON: {json}",
                ex);
        }
    }


    // =========================================================
    // EINZELNES BUCH
    // =========================================================

    public async Task<BibleBook?> GetBookAsync(
        string language,
        int bookNumber)
    {
        language =
            NormalizeLanguage(language);

        // -----------------------------------------------------
        // Buch-Metadaten laden
        // -----------------------------------------------------

        var booksResponse =
            await GetBooksAsync(language);

        if (booksResponse == null)
        {
            return null;
        }

        var book =
            booksResponse.Books
                .FirstOrDefault(
                    b =>
                        b.Book_Number ==
                        bookNumber);

        if (book == null)
        {
            return null;
        }

        // -----------------------------------------------------
        // Wenn Kapitel bereits vorhanden sind
        // -----------------------------------------------------

        if (book.Chapters.Count > 0)
        {
            return book;
        }

        // -----------------------------------------------------
        // Original-JSON erneut laden
        // -----------------------------------------------------

        var booksUrl =
            $"books?language={Uri.EscapeDataString(language)}";

        var booksResponseHttp =
            await _httpClient.GetAsync(
                booksUrl);

        var booksJson =
            await booksResponseHttp.Content
                .ReadAsStringAsync();

        if (!booksResponseHttp.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bible API returned " +
                $"{(int)booksResponseHttp.StatusCode}: " +
                booksJson);
        }

        using var document =
            JsonDocument.Parse(
                booksJson);

        var root =
            document.RootElement;

        if (!root.TryGetProperty(
                "books",
                out var booksElement) ||
            booksElement.ValueKind !=
                JsonValueKind.Array)
        {
            return book;
        }

        var bookElement =
            booksElement
                .EnumerateArray()
                .FirstOrDefault(
                    b =>
                        GetInt(
                            b,
                            "book_number") ==
                        bookNumber);

        if (bookElement.ValueKind !=
            JsonValueKind.Object)
        {
            return book;
        }

        var chapterCount =
            GetInt(
                bookElement,
                "chapters");

        if (chapterCount <= 0)
        {
            return book;
        }

        // -----------------------------------------------------
        // Kapitel einzeln laden
        // -----------------------------------------------------

        for (
            var chapterNumber = 1;
            chapterNumber <= chapterCount;
            chapterNumber++)
        {
            try
            {
                /*
                 * WICHTIG:
                 *
                 * Die HolyBible API erwartet:
                 *
                 * Jes.41:1-999
                 * John.14:1-999
                 * Ис.41:1-999
                 *
                 * und NICHT:
                 *
                 * Jes41:1-999
                 * John14:1-999
                 */

                var search =
                    $"{book.Short_Name}." +
                    $"{chapterNumber}:1-999";

                var passage =
                    await GetPassageAsync(
                        language,
                        search);

                if (passage == null ||
                    passage.Books == null ||
                    passage.Books.Count == 0)
                {
                    continue;
                }

                var resultBook =
                    passage.Books
                        .FirstOrDefault(
                            b =>
                                b.Book_Number ==
                                bookNumber);

                if (resultBook == null)
                {
                    continue;
                }

                foreach (
                    var resultChapter
                    in resultBook.Chapters)
                {
                    if (resultChapter.Chapter_Number !=
                        chapterNumber)
                    {
                        continue;
                    }

                    var existingChapter =
                        book.Chapters
                            .FirstOrDefault(
                                c =>
                                    c.Chapter_Number ==
                                    chapterNumber);

                    if (existingChapter == null)
                    {
                        book.Chapters.Add(
                            resultChapter);
                    }
                    else
                    {
                        foreach (
                            var verse
                            in resultChapter.Verses)
                        {
                            if (!existingChapter.Verses
                                    .Any(
                                        v =>
                                            v.Verse ==
                                            verse.Verse))
                            {
                                existingChapter.Verses
                                    .Add(verse);
                            }
                        }
                    }
                }
            }
            catch (HttpRequestException)
            {
                // Ein einzelnes Kapitel darf
                // nicht das gesamte Buch zerstören.
                continue;
            }
            catch (JsonException)
            {
                continue;
            }
        }

        // -----------------------------------------------------
        // Kapitel sortieren
        // -----------------------------------------------------

        book.Chapters =
            book.Chapters
                .OrderBy(
                    c =>
                        c.Chapter_Number)
                .ToList();

        // -----------------------------------------------------
        // Verse sortieren
        // -----------------------------------------------------

        foreach (var chapter
                 in book.Chapters)
        {
            chapter.Verses =
                chapter.Verses
                    .OrderBy(
                        v =>
                            v.Verse)
                    .ToList();
        }

        return book;
    }


    // =========================================================
    // PASSAGE / VERSE
    // =========================================================

    public async Task<BibleRandomResponse?> GetPassageAsync(
        string language,
        string search)
    {
        language =
            NormalizeLanguage(language);

        if (string.IsNullOrWhiteSpace(search))
        {
            return null;
        }

        var url =
            $"find?language={Uri.EscapeDataString(language)}" +
            $"&search={Uri.EscapeDataString(search)}";

        var response =
            await _httpClient.GetAsync(url);

        var json =
            await response.Content
                .ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Bible API returned " +
                $"{(int)response.StatusCode}: {json}");
        }

        return DeserializeResponse<BibleRandomResponse>(
            json);
    }


    // =========================================================
    // KAPITEL AUS JSON LESEN
    // =========================================================

    private static void ReadChapters(
        BibleBook book,
        JsonElement chaptersElement)
    {
        foreach (
            var chapterElement
            in chaptersElement.EnumerateArray())
        {
            if (chapterElement.ValueKind !=
                JsonValueKind.Object)
            {
                continue;
            }

            var chapter =
                new BibleChapter
                {
                    Chapter_Number =
                        GetInt(
                            chapterElement,
                            "chapter_number")
                };

            if (chapterElement.TryGetProperty(
                    "verses",
                    out var versesElement) &&
                versesElement.ValueKind ==
                    JsonValueKind.Array)
            {
                foreach (
                    var verseElement
                    in versesElement.EnumerateArray())
                {
                    if (verseElement.ValueKind !=
                        JsonValueKind.Object)
                    {
                        continue;
                    }

                    chapter.Verses.Add(
                        new BibleVerse
                        {
                            Verse =
                                GetInt(
                                    verseElement,
                                    "verse"),

                            Text =
                                GetString(
                                    verseElement,
                                    "text")
                        });
                }
            }

            book.Chapters.Add(
                chapter);
        }
    }


    // =========================================================
    // JSON DESERIALISIERUNG
    // =========================================================

    private static T? DeserializeResponse<T>(
        string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        try
        {
            return JsonSerializer.Deserialize<T>(
                json,
                JsonOptions);
        }
        catch (JsonException ex)
        {
            throw new JsonException(
                $"Die Bible API lieferte JSON, das nicht in " +
                $"{typeof(T).Name} umgewandelt werden konnte." +
                Environment.NewLine +
                $"JSON: {json}",
                ex);
        }
    }


    // =========================================================
    // SPRACHE NORMALISIEREN
    // =========================================================

    private static string NormalizeLanguage(
        string? language)
    {
        if (string.IsNullOrWhiteSpace(language))
        {
            return "de";
        }

        return language
            .Trim()
            .ToLowerInvariant();
    }


    // =========================================================
    // JSON STRING HELPER
    // =========================================================

    private static string GetString(
        JsonElement element,
        string propertyName)
    {
        if (!element.TryGetProperty(
                propertyName,
                out var property))
        {
            return string.Empty;
        }

        return property.ValueKind switch
        {
            JsonValueKind.String =>
                property.GetString()
                ?? string.Empty,

            JsonValueKind.Null =>
                string.Empty,

            _ =>
                property.ToString()
        };
    }


    // =========================================================
    // JSON INT HELPER
    // =========================================================

    private static int GetInt(
        JsonElement element,
        string propertyName)
    {
        if (!element.TryGetProperty(
                propertyName,
                out var property))
        {
            return 0;
        }

        if (property.ValueKind ==
                JsonValueKind.Number &&
            property.TryGetInt32(
                out var numberValue))
        {
            return numberValue;
        }

        if (property.ValueKind ==
                JsonValueKind.String &&
            int.TryParse(
                property.GetString(),
                out var stringValue))
        {
            return stringValue;
        }

        return 0;
    }
}