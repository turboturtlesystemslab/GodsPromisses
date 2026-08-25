using GodsPromises.Api.Models;
using GodsPromises.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GodsPromises.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BibleController : ControllerBase
{
    private readonly BibleService _bibleService;

    public BibleController(BibleService bibleService)
    {
        _bibleService = bibleService;
    }

    // ============================================================
    // GET /api/Bible/books?language=ru
    // ============================================================

    [HttpGet("books")]
    public async Task<IActionResult> GetBooks(
        [FromQuery] string language = "de")
    {
        language = language.Trim().ToLowerInvariant();

        try
        {
            var result =
                await _bibleService.GetBooksAsync(language);

            if (result == null)
            {
                return NotFound(new
                {
                    error = "NoBooksFound",
                    language
                });
            }

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                error = "BibleApiError",
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "InternalError",
                message = ex.Message
            });
        }
    }


    // ============================================================
    // GET /api/Bible/book?language=ru&bookNumber=290
    // ============================================================

    [HttpGet("book")]
    public async Task<IActionResult> GetBook(
        [FromQuery] string language = "de",
        [FromQuery] int bookNumber = 0)
    {
        language = language.Trim().ToLowerInvariant();

        if (bookNumber <= 0)
        {
            return BadRequest(new
            {
                error = "InvalidBookNumber",
                message = "bookNumber muss größer als 0 sein."
            });
        }

        try
        {
            var result =
                await _bibleService.GetBookAsync(
                    language,
                    bookNumber);

            if (result == null)
            {
                return NotFound(new
                {
                    error = "BookNotFound",
                    language,
                    bookNumber
                });
            }

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                error = "BibleApiError",
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "InternalError",
                message = ex.Message
            });
        }
    }


    // ============================================================
    // GET /api/Bible/passage
    //
    // Beispiel:
    // /api/Bible/passage?language=ru&search=Ис.%41%3A10
    // ============================================================

    [HttpGet("passage")]
    public async Task<IActionResult> GetPassage(
        [FromQuery] string language = "de",
        [FromQuery] string search = "")
    {
        language = language.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(search))
        {
            return BadRequest(new
            {
                error = "MissingSearch",
                message = "search darf nicht leer sein."
            });
        }

        try
        {
            var result =
                await _bibleService.GetPassageAsync(
                    language,
                    search);

            if (result == null)
            {
                return NotFound(new
                {
                    error = "PassageNotFound",
                    language,
                    search
                });
            }

            return Ok(result);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(502, new
            {
                error = "BibleApiError",
                message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                error = "InternalError",
                message = ex.Message
            });
        }
    }
}