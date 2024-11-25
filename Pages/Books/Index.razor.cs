using Bogus;
using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Microsoft.AspNetCore.Components;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private string _selectedLocale = LocaleHelper.GetDefault();

    private string SelectedLocale
    {
        get => _selectedLocale;
        set { _selectedLocale = value; }
    }

    private int LikesMult10 { get; set; } = 50;

    private double Likes
    {
        get => LikesMult10 / 10.0;
    }

    private int _seed = 12345;

    private int Seed
    {
        get => _seed;
        set { _seed = value; }
    }

    private double _reviews = 5;

    private double Reviews
    {
        get => _reviews;
        set { _reviews = value; }
    }

    private IEnumerable<Book> Books { get; set; } = [];

    private string? ExpandedBook { get; set; } = null;

    private bool IsLoading = false;

    private Faker _faker = null!;

    protected override Task OnInitializedAsync()
    {
        System.Console.WriteLine("Initialized");
        Randomizer.Seed = new Random(8675309);
        _faker = new Faker(LocaleHelper.GetCode(SelectedLocale));
        Books = Book.GenerateBooks(20, _faker, Likes, Reviews).ToList();
        return Task.CompletedTask;
    }

    private void ToggleDetails(string isbn)
    {
        System.Console.WriteLine("asdfasdfasd222222222222");
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private void RandomizeSeed()
    {
        Seed = new Random().Next(0, 100000);
    }

    // private ValueTask<ItemsProviderResult<Book>> LoadBooks(ItemsProviderRequest request)
    // {
    //     int startIndex = request.StartIndex;
    //     int count = request.Count;
    //
    //     var faker = new Faker(LocaleHelper.GetCode(SelectedLocale));
    //     Randomizer.Seed = new Random(SeedPageCombine(SeedValue, startIndex / count));
    //
    //     var Books = Book.GenerateBooks(count, faker, LikesPerBook, ReviewPerBook);
    //
    //     return new ValueTask<ItemsProviderResult<Book>>(
    //         new ItemsProviderResult<Book>(Books, int.MaxValue)
    //     );
    // }

    // TODO: Убрать это куда-нибудь
    private static int SeedPageCombine(int seed, int page)
    {
        // TODO: Make it better
        return seed + page;
    }
}
