using Bogus;
using BookStoreTester.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/books")]
[ApiController]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks(int seed, int page, int amount, int likes, int reviews)
    {
        var bookSeed = SeedPageCombine(seed, page);
        Randomizer.Seed = new Random(bookSeed);
        List<BookDto> books = Book.GenerateBooks(amount, likes, reviews)
            .Select(b => b.ToDto())
            .ToList();
        return Ok(books);
    }

    private static int SeedPageCombine(int seed, int page)
    {
        return HashCode.Combine(seed, page);
    }
}
