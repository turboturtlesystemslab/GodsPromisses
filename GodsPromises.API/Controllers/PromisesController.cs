using GodsPromises.Api.Models;
using GodsPromises.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GodsPromises.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PromisesController : ControllerBase
{
    private readonly BibleService _bibleService;

    public PromisesController(BibleService bibleService)
    {
        _bibleService = bibleService;
    }

    // ============================================================
    // VERHEISSUNGEN
    // ============================================================

    private static readonly List<PromiseReference> PromiseReferences =
    [
        new()
        {
            Id = 1,
            BookNumber = 290,
            Chapter = 41,
            Verse = 10,

            // Interne Theme-ID
            Theme = "trust",

            References = new()
            {
                ["de"] = "Jesaja 41,10",
                ["en"] = "Isaiah 41:10",
                ["fr"] = "Ésaïe 41:10",
                ["es"] = "Isaías 41:10",
                ["it"] = "Isaia 41:10",
                ["pt"] = "Isaías 41:10",
                ["ru"] = "Исаия 41:10",
                ["uk"] = "Ісаї 41:10",
                ["ro"] = "Isaia 41:10"
            },

            SearchNames = new()
            {
                ["de"] = "Jes.",
                ["en"] = "Isa.",
                ["fr"] = "Es.",
                ["es"] = "Is.",
                ["it"] = "Is.",
                ["pt"] = "Is.",
                ["ru"] = "Ис.",
                ["uk"] = "Іс.",
                ["ro"] = "Is."
            }
        },

        new()
        {
            Id = 2,
            BookNumber = 300,
            Chapter = 29,
            Verse = 11,

            Theme = "hope",

            References = new()
            {
                ["de"] = "Jeremia 29,11",
                ["en"] = "Jeremiah 29:11",
                ["fr"] = "Jérémie 29:11",
                ["es"] = "Jeremías 29:11",
                ["it"] = "Geremia 29:11",
                ["pt"] = "Jeremias 29:11",
                ["ru"] = "Иеремия 29:11",
                ["uk"] = "Єремії 29:11",
                ["ro"] = "Ieremia 29:11"
            },

            SearchNames = new()
            {
                ["de"] = "Jer.",
                ["en"] = "Jer.",
                ["fr"] = "Jér.",
                ["es"] = "Jer.",
                ["it"] = "Ger.",
                ["pt"] = "Jer.",
                ["ru"] = "Иер.",
                ["uk"] = "Єр.",
                ["ro"] = "Ier."
            }
        },

        new()
        {
            Id = 3,
            BookNumber = 60,
            Chapter = 1,
            Verse = 9,

            Theme = "courage",

            References = new()
            {
                ["de"] = "Josua 1,9",
                ["en"] = "Joshua 1:9",
                ["fr"] = "Josué 1:9",
                ["es"] = "Josué 1:9",
                ["it"] = "Giosuè 1:9",
                ["pt"] = "Josué 1:9",
                ["ru"] = "Иисус Навин 1:9",
                ["uk"] = "Ісуса Навина 1:9",
                ["ro"] = "Iosua 1:9"
            },

            SearchNames = new()
            {
                ["de"] = "Jos.",
                ["en"] = "Josh.",
                ["fr"] = "Jos.",
                ["es"] = "Jos.",
                ["it"] = "Gs.",
                ["pt"] = "Js.",
                ["ru"] = "Нав.",
                ["uk"] = "Іс.Нав.",
                ["ro"] = "Ios."
            }
        },

        new()
        {
            Id = 4,
            BookNumber = 570,
            Chapter = 4,
            Verse = 13,

            Theme = "strength",

            References = new()
            {
                ["de"] = "Philipper 4,13",
                ["en"] = "Philippians 4:13",
                ["fr"] = "Philippiens 4:13",
                ["es"] = "Filipenses 4:13",
                ["it"] = "Filippesi 4:13",
                ["pt"] = "Filipenses 4:13",
                ["ru"] = "Филиппийцам 4:13",
                ["uk"] = "Филип'ян 4:13",
                ["ro"] = "Filipeni 4:13"
            },

            SearchNames = new()
            {
                ["de"] = "Phil.",
                ["en"] = "Phil.",
                ["fr"] = "Phil.",
                ["es"] = "Fil.",
                ["it"] = "Fil.",
                ["pt"] = "Fp.",
                ["ru"] = "Флп.",
                ["uk"] = "Флп.",
                ["ro"] = "Filip."
            }
        },

        new()
        {
            Id = 5,
            BookNumber = 230,
            Chapter = 23,
            Verse = 1,

            Theme = "security",

            References = new()
            {
                ["de"] = "Psalm 23,1",
                ["en"] = "Psalm 23:1",
                ["fr"] = "Psaume 23:1",
                ["es"] = "Salmo 23:1",
                ["it"] = "Salmo 23:1",
                ["pt"] = "Salmo 23:1",
                ["ru"] = "Псалом 23:1",
                ["uk"] = "Псалом 23:1",
                ["ro"] = "Psalmul 23:1"
            },

            SearchNames = new()
            {
                ["de"] = "Ps.",
                ["en"] = "Ps.",
                ["fr"] = "Ps.",
                ["es"] = "Sal.",
                ["it"] = "Sal.",
                ["pt"] = "Sl.",
                ["ru"] = "Пс.",
                ["uk"] = "Пс.",
                ["ro"] = "Ps."
            }
        },

        new()
        {
            Id = 6,
            BookNumber = 500,
            Chapter = 14,
            Verse = 27,

            Theme = "peace",

            References = new()
            {
                ["de"] = "Johannes 14,27",
                ["en"] = "John 14:27",
                ["fr"] = "Jean 14:27",
                ["es"] = "Juan 14:27",
                ["it"] = "Giovanni 14:27",
                ["pt"] = "João 14:27",
                ["ru"] = "Иоанна 14:27",
                ["uk"] = "Івана 14:27",
                ["ro"] = "Ioan 14:27"
            },

            SearchNames = new()
            {
                ["de"] = "Joh.",
                ["en"] = "Jn.",
                ["fr"] = "Jn.",
                ["es"] = "Jn.",
                ["it"] = "Gv.",
                ["pt"] = "Jo.",
                ["ru"] = "Ин.",
                ["uk"] = "Ів.",
                ["ro"] = "Ioan"
            }
        },

        new()
        {
            Id = 7,
            BookNumber = 230,
            Chapter = 34,
            Verse = 19,

            Theme = "comfort",

            References = new()
            {
                ["de"] = "Psalm 34,19",
                ["en"] = "Psalm 34:19",
                ["fr"] = "Psaume 34:19",
                ["es"] = "Salmo 34:19",
                ["it"] = "Salmo 34:19",
                ["pt"] = "Salmo 34:19",
                ["ru"] = "Псалом 34:19",
                ["uk"] = "Псалом 34:19",
                ["ro"] = "Psalmul 34:19"
            },

            SearchNames = new()
            {
                ["de"] = "Ps.",
                ["en"] = "Ps.",
                ["fr"] = "Ps.",
                ["es"] = "Sal.",
                ["it"] = "Sal.",
                ["pt"] = "Sl.",
                ["ru"] = "Пс.",
                ["uk"] = "Пс.",
                ["ro"] = "Ps."
            }
        },

        new()
        {
            Id = 8,
            BookNumber = 520,
            Chapter = 8,
            Verse = 28,

            Theme = "hope",

            References = new()
            {
                ["de"] = "Römer 8,28",
                ["en"] = "Romans 8:28",
                ["fr"] = "Romains 8:28",
                ["es"] = "Romanos 8:28",
                ["it"] = "Romani 8:28",
                ["pt"] = "Romanos 8:28",
                ["ru"] = "Римлянам 8:28",
                ["uk"] = "Римлян 8:28",
                ["ro"] = "Romani 8:28"
            },

            SearchNames = new()
            {
                ["de"] = "Röm.",
                ["en"] = "Rom.",
                ["fr"] = "Rom.",
                ["es"] = "Rom.",
                ["it"] = "Rom.",
                ["pt"] = "Rm.",
                ["ru"] = "Рим.",
                ["uk"] = "Рим.",
                ["ro"] = "Rom."
            }
        }
    ];


    // ============================================================
    // GET /api/Promises/random?language=de
    // ============================================================

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomPromise(
        [FromQuery] string language = "de")
    {
        language = NormalizeLanguage(language);

        if (!IsSupportedLanguage(language))
        {
            return UnsupportedLanguage(language);
        }

        var shuffledPromises = PromiseReferences
            .OrderBy(_ => Random.Shared.Next())
            .ToList();

        foreach (var selected in shuffledPromises)
        {
            var promise = await LoadPromiseAsync(selected, language);

            if (promise != null)
            {
                return Ok(promise);
            }
        }

        return NoPromiseFound(language);
    }


    // ============================================================
    // GET /api/Promises/theme/hope?language=de
    // ============================================================

    [HttpGet("theme/{theme}")]
    public async Task<IActionResult> GetPromiseByTheme(
        string theme,
        [FromQuery] string language = "de")
    {
        language = NormalizeLanguage(language);
        theme = NormalizeTheme(theme);

        if (!IsSupportedLanguage(language))
        {
            return UnsupportedLanguage(language);
        }

        if (!IsSupportedTheme(theme))
        {
            return BadRequest(new
            {
                error = "UnsupportedTheme",
                theme,
                language,
                message =
                    "Dieses Thema wird von God's Promises momentan nicht unterstützt."
            });
        }

        var matchingPromises = PromiseReferences
            .Where(p =>
                p.Theme.Equals(
                    theme,
                    StringComparison.OrdinalIgnoreCase))
            .OrderBy(_ => Random.Shared.Next())
            .ToList();

        foreach (var selected in matchingPromises)
        {
            var promise = await LoadPromiseAsync(selected, language);

            if (promise != null)
            {
                return Ok(promise);
            }
        }

        return NoPromiseFound(language, theme);
    }


    // ============================================================
    // GET /api/Promises/{id}?language=de
    // ============================================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPromiseById(
        int id,
        [FromQuery] string language = "de")
    {
        language = NormalizeLanguage(language);

        if (!IsSupportedLanguage(language))
        {
            return UnsupportedLanguage(language);
        }

        var selected = PromiseReferences
            .FirstOrDefault(p => p.Id == id);

        if (selected == null)
        {
            return NotFound(new
            {
                error = "PromiseNotFound",
                id,
                message =
                    "Diese Verheißung wurde nicht gefunden."
            });
        }

        var promise = await LoadPromiseAsync(selected, language);

        if (promise != null)
        {
            return Ok(promise);
        }

        return NotFound(new
        {
            error = "VerseNotFound",
            id,
            language,
            reference = selected.References[language],
            message =
                "Der gewünschte Bibelvers wurde nicht gefunden."
        });
    }


    // ============================================================
    // GEMEINSAME PROMISE-LOGIK
    // ============================================================

    private async Task<Promise?> LoadPromiseAsync(
        PromiseReference selected,
        string language)
    {
        if (!selected.SearchNames.TryGetValue(
                language,
                out var shortName))
        {
            return null;
        }

        try
        {
            var search =
                $"{shortName}{selected.Chapter}:{selected.Verse}";

            var bibleResult =
                await _bibleService.GetPassageAsync(
                    language,
                    search);

            if (bibleResult == null ||
                bibleResult.Books == null ||
                bibleResult.Books.Count == 0)
            {
                return null;
            }

            var resultBook = bibleResult.Books[0];

            if (resultBook.Chapters == null ||
                resultBook.Chapters.Count == 0)
            {
                return null;
            }

            var chapter = resultBook.Chapters[0];

            if (chapter.Verses == null ||
                chapter.Verses.Count == 0)
            {
                return null;
            }

            var text = string.Join(
                " ",
                chapter.Verses.Select(v => v.Text));

            text = CleanBibleText(text);

            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }

            return new Promise
            {
                Id = selected.Id,
                Reference = selected.References[language],
                Text = text,
                Theme = selected.Theme,
                Language = language
            };
        }
        catch
        {
            return null;
        }
    }


    // ============================================================
    // HILFSFUNKTIONEN
    // ============================================================

    private static string NormalizeLanguage(string? language)
    {
        return string.IsNullOrWhiteSpace(language)
            ? "de"
            : language.Trim().ToLowerInvariant();
    }

    private static string NormalizeTheme(string? theme)
    {
        return string.IsNullOrWhiteSpace(theme)
            ? string.Empty
            : theme.Trim().ToLowerInvariant();
    }

    private static bool IsSupportedLanguage(string language)
    {
        return new[]
        {
            "de",
            "en",
            "fr",
            "es",
            "it",
            "pt",
            "ru",
            "uk",
            "ro"
        }.Contains(language);
    }

    private static bool IsSupportedTheme(string theme)
    {
        return new[]
        {
            "trust",
            "hope",
            "courage",
            "strength",
            "security",
            "peace",
            "comfort"
        }.Contains(theme);
    }

    private IActionResult UnsupportedLanguage(string language)
    {
        return BadRequest(new
        {
            error = "UnsupportedLanguage",
            language,
            message =
                "Diese Sprache wird von God's Promises momentan nicht unterstützt."
        });
    }

    private IActionResult NoPromiseFound(
        string language,
        string? theme = null)
    {
        return NotFound(new
        {
            error = "NoPromiseFound",
            language,
            theme,
            message =
                "Für diese Auswahl konnte momentan keine passende Verheißung gefunden werden."
        });
    }


    // ============================================================
    // BIBLE API TEXT BEREINIGEN
    // ============================================================

    private static string CleanBibleText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        text = Regex.Replace(
            text,
            @"<[^>]*>",
            ""
        );

        text = text
            .Replace("&quot;", "\"")
            .Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&nbsp;", " ");

        text = Regex.Replace(
            text,
            @"\[\s*\d+\s*\]",
            ""
        );

        text = Regex.Replace(
            text,
            @"[\u24D0-\u24E9]",
            ""
        );

        text = Regex.Replace(
            text,
            @"[\u24B6-\u24CF]",
            ""
        );

        text = Regex.Replace(
            text,
            @"[\u2460-\u2473]",
            ""
        );

        text = Regex.Replace(
            text,
            @"[\u24F5-\u24FF]",
            ""
        );

        text = Regex.Replace(
            text,
            @"[\u00B9\u00B2\u00B3\u2070-\u2079]+",
            ""
        );

        text = Regex.Replace(
            text,
            @"ⓐ|ⓑ|ⓒ|ⓓ|ⓔ|ⓕ|ⓖ|ⓗ|ⓘ|ⓙ|ⓚ|ⓛ|ⓜ|ⓝ|ⓞ|ⓟ|ⓠ|ⓡ|ⓢ|ⓣ|ⓤ|ⓥ|ⓦ|ⓧ|ⓨ|ⓩ",
            ""
        );

        text = Regex.Replace(
            text,
            @"(?<=\s)\d{1,5}(?=[\.,;:!?])",
            ""
        );

        text = Regex.Replace(
            text,
            @"\s+",
            " "
        );

        text = Regex.Replace(
            text,
            @"\s+([,.;:!?])",
            "$1"
        );

        text = Regex.Replace(
            text,
            @"\(\s+",
            "("
        );

        text = Regex.Replace(
            text,
            @"\s+\)",
            ")"
        );

        return text.Trim();
    }
}


// ================================================================
// Promise Reference
// ================================================================

public class PromiseReference
{
    public int Id { get; set; }

    public int BookNumber { get; set; }

    public int Chapter { get; set; }

    public int Verse { get; set; }

    // Interne ID, NICHT die sichtbare Übersetzung.
    public string Theme { get; set; } = string.Empty;

    public Dictionary<string, string> References { get; set; } = [];

    public Dictionary<string, string> SearchNames { get; set; } = [];
}