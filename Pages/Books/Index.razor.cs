using Bogus;
using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private Virtualize<Book> _virtualizeRef;

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
                book.setReviews(Times.ToInt(value));
            }
            _virtualizeRef.RefreshDataAsync();
        }
    }

    private IEnumerable<Book> Books { get; set; } = [];

    private string? ExpandedBook { get; set; } = null;

    private bool IsLoading = false;

    private Faker _faker = null!;

    private void ToggleDetails(string isbn)
    {
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private void RandomizeSeed()
    {
        Seed = new Random().Next(0, 100000);
    }

    private ValueTask<ItemsProviderResult<Book>> LoadBooks(ItemsProviderRequest request)
    {
        Console.WriteLine($"StartIndex: {request.StartIndex}, Count: {request.Count}");

        var books = Enumerable
            .Range(request.StartIndex, request.Count)
            .Select(index =>
            {
                var bookSeed = SeedPageCombine(Seed, index);
                Randomizer.Seed = new Random(bookSeed);
                return new Book(Times.ToInt(Likes), Times.ToInt(Reviews));
            })
            .ToList();

        Console.WriteLine($"Generated {books.Count()} books:");
        foreach (var book in books)
        {
            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}");
        }

        return new ValueTask<ItemsProviderResult<Book>>(new ItemsProviderResult<Book>(books, 1000));
    }

    private static int SeedPageCombine(int seed, int page)
    {
        return HashCode.Combine(seed, page);
    }
}
