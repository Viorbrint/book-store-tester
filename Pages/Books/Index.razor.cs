using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private Virtualize<BookDto> _virtualizeRef;

    private string _selectedLocale = LocaleHelper.GetDefault();

    private string SelectedLocale
    {
        get => _selectedLocale;
        set
        {
            _selectedLocale = value;
            _virtualizeRef.RefreshDataAsync();
        }
    }

    private int _likesMult10 = 50;
    private int LikesMult10
    {
        get => _likesMult10;
        set
        {
            _likesMult10 = value;
            foreach (var book in Books)
            {
                book.Likes = Times.ToInt(_likesMult10 / 10.0);
            }
            _virtualizeRef.RefreshDataAsync();
        }
    }

    private double Likes
    {
        get => LikesMult10 / 10.0;
    }

    private int _seed = 12345;

    private int Seed
    {
        get => _seed;
        set
        {
            _seed = value;
            _virtualizeRef.RefreshDataAsync();
        }
    }

    private double _reviews = 5;

    private double Reviews
    {
        get => _reviews;
        set
        {
            _reviews = value;
            foreach (var book in Books)
            {
                book.Reviews = Review
                    .GenerateReviews(Times.ToInt(value))
                    .Select(r => r.ToDto())
                    .ToList();
            }
            _virtualizeRef.RefreshDataAsync();
        }
    }

    private IEnumerable<BookDto> Books { get; set; } = [];

    private string? ExpandedBook { get; set; } = null;

    private bool IsLoading = false;

    private void ToggleDetails(string isbn)
    {
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private void RandomizeSeed()
    {
        Seed = new Random().Next(0, 100000);
    }

    private async ValueTask<ItemsProviderResult<BookDto>> LoadBooks(ItemsProviderRequest request)
    {
        var books = await booksRequest(request.StartIndex / request.Count, request.Count);

        Console.WriteLine($"Generated {books.Count()} books:");
        foreach (var book in books)
        {
            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}");
        }

        return new ItemsProviderResult<BookDto>(books, 1000);
    }

    [Inject]
    private HttpClient Http { get; set; } = null!;

    private async Task<List<BookDto>> booksRequest(int page, int amount)
    {
        return await Http.GetFromJsonAsync<List<BookDto>>(
                $"books?seed={Seed}&page={page}&amount={amount}&likes={Likes}&reviews={Reviews}"
            ) ?? new List<BookDto>();
    }

    private static int SeedPageCombine(int seed, int page)
    {
        return HashCode.Combine(seed, page);
    }
}
