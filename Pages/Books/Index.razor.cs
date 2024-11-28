using Bogus;
using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Csv;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private const int FirstPage = 1;

    private const int PageSize = 20;

    private const int DefaultLikesMult10 = 50;

    private const int DefaultSeed = 12345;

    private const int DefaultReviews = 5;

    private int Page { get; set; } = FirstPage;

    private bool IsGalleryView { get; set; } = false;

    private string _selectedLocale = LocaleHelper.GetDefault();

    private string SelectedLocale
    {
        get => _selectedLocale;
        set
        {
            _selectedLocale = value;
            Faker = new Faker(LocaleHelper.GetCode(value));
            ResetBooks();
        }
    }

    private int _likesMult10 = DefaultLikesMult10;

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
        }
    }

    private double Likes
    {
        get => LikesMult10 / 10.0;
    }

    private int _seed = DefaultSeed;

    private int Seed
    {
        get => _seed;
        set
        {
            _seed = value;
            ResetBooks();
        }
    }

    private double _reviews = DefaultReviews;

    private double Reviews
    {
        get => _reviews;
        set
        {
            _reviews = value;
            foreach (var book in Books)
            {
                book.setReviews(Times.ToInt(value), Faker);
            }
        }
    }

    private List<Book> Books { get; set; } = [];

    [Inject]
    private ImageService ImageService { get; set; } = null!;

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    private string? ExpandedBook { get; set; } = null;

    private Faker Faker { get; set; } = new Faker(LocaleHelper.GetCode(LocaleHelper.GetDefault()));

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LoadMoreBooks();
    }

    private void ToggleView(bool isGallery)
    {
        IsGalleryView = isGallery;
    }

    private async Task ExportToCSV()
    {
        const string Filename = "books.csv";
        const string FuncName = "saveAsFile";
        var csv = new CsvExport();
        csv.AddRows(Books.Select(book => new BookCsv(book)));
        string export = csv.Export();
        await JSRuntime.InvokeVoidAsync(FuncName, Filename, export);
    }

    private void ToggleDetails(string isbn)
    {
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private void RandomizeSeed()
    {
        const int min = 0;
        const int max = 100000;
        Seed = new Random().Next(min, max);
    }

    private void LoadMoreBooks()
    {
        SetRandom();
        var newBooks = Book.GenerateBooks(
            PageSize,
            Times.ToInt(Likes),
            Times.ToInt(Reviews),
            Faker,
            ImageService
        );
        Books.AddRange(newBooks);
        Page++;
        StateHasChanged();
    }

    private void SetRandom()
    {
        var bookSeed = CombineSeedPage(Seed, Page);
        Randomizer.Seed = new Random(bookSeed);
        Faker = new Faker(LocaleHelper.GetCode(SelectedLocale));
    }

    private int CombineSeedPage(int seed, int page)
    {
        const int mult = 31;
        return seed + page * mult;
    }

    private void ResetBooks()
    {
        Page = FirstPage;
        Books = [];

        LoadMoreBooks();
    }

    [JSInvokable("OnScrollAsync")]
    public static void OnScrollAsync()
    {
        var instance = Instance;
        if (instance != null)
        {
            instance.LoadMoreBooks();
        }
    }

    private static Index? Instance { get; set; }

    protected override void OnInitialized()
    {
        Instance = this;
    }

    public void Dispose()
    {
        Instance = null;
    }
}
