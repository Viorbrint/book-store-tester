using Bogus;
using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private string _selectedLocale = LocaleHelper.GetDefault();

    private string SelectedLocale
    {
        get => _selectedLocale;
        set
        {
            _selectedLocale = value;
            // Books = GenerateBooks();
        }
    }

    private List<string> AvailableLocales { get; } = LocaleHelper.GetAvailable();

    private double _likesPerBook = 0;

    private double LikesPerBook
    {
        get => _likesPerBook;
        set => _likesPerBook = value / 10;
    }

    private double ReviewPerBook = 5;

    public IEnumerable<Book> Books { get; private set; } = [];
    private string? ExpandedBook = null;
    private bool IsLoading = false;

    private Faker _faker = new Faker(LocaleHelper.GetCode(LocaleHelper.GetDefault()));

    private void setFaker(string localeCode)
    {
        _faker = new Faker(localeCode);
    }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;
        Books = Book.GenerateBooks(50, _faker, LikesPerBook, ReviewPerBook);
        IsLoading = false;
    }

    private void ToggleDetails(string isbn)
    {
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private string GetAuthorsString(IEnumerable<string> authors) => string.Join(", ", authors);

    private int _seedValue = 12345;

    private int SeedValue
    {
        get => _seedValue;
        set
        {
            _seedValue = value;
            // Books = GenerateBooks();
        }
    }

    private void RandomizeSeed()
    {
        SeedValue = new Random().Next(0, 100000);
    }

    private ValueTask<ItemsProviderResult<Book>> LoadBooks(ItemsProviderRequest request)
    {
        int startIndex = request.StartIndex;
        int count = request.Count;

        var faker = new Faker(LocaleHelper.GetCode(SelectedLocale));
        Randomizer.Seed = new Random(SeedPageCombine(SeedValue, startIndex / count));

        var Books = Book.GenerateBooks(count, faker, LikesPerBook, ReviewPerBook);

        return new ValueTask<ItemsProviderResult<Book>>(
            new ItemsProviderResult<Book>(Books, int.MaxValue)
        );
    }

    // TODO: Убрать это куда-нибудь
    private static int SeedPageCombine(int seed, int page)
    {
        // TODO: Make it better
        return seed + page;
    }
}
