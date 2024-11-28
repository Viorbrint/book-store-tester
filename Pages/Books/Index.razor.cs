using Bogus;
using BookStoreTester.Helpers;
using BookStoreTester.Models;
using Csv;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStoreTester.Pages.Books;

public partial class Index : ComponentBase
{
    private bool IsGalleryView { get; set; } = false;

    private void ToggleView(bool isGallery)
    {
        IsGalleryView = isGallery;
    }

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
            ResetBooks();
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
                book.setReviews(Times.ToInt(value), Faker);
            }
        }
    }

    private List<Book> Books { get; set; } = [];

    [Inject]
    private IJSRuntime JSRuntime { get; set; } = null!;

    private string? ExpandedBook { get; set; } = null;

    private Faker Faker { get; set; } = new Faker(LocaleHelper.GetCode(LocaleHelper.GetDefault()));

    private async Task ExportToCSV()
    {
        var csv = new CsvExport();
        csv.AddRows(Books.Select(book => new BookCsv(book)));
        string export = csv.Export();
        await JSRuntime.InvokeVoidAsync("saveAsFile", "books.csv", export);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        LoadMoreBooks();
    }

    private void ToggleDetails(string isbn)
    {
        ExpandedBook = ExpandedBook == isbn ? null : isbn;
    }

    private void RandomizeSeed()
    {
        Seed = new Random().Next(0, 100000);
    }

    private int _page = 1;

    private int Page
    {
        get => _page;
        set { _page = value; }
    }

    private const int PageSize = 20;

    private void LoadMoreBooks()
    {
        SetRandom();
        var newBooks = Book.GenerateBooks(
            PageSize,
            Times.ToInt(Likes),
            Times.ToInt(Reviews),
            Faker
        );
        Books.AddRange(newBooks);
        Page++;
        StateHasChanged();
    }

    private void SetRandom()
    {
        var bookSeed = Seed ^ Page;
        Randomizer.Seed = new Random(bookSeed);
        Faker = new Faker(LocaleHelper.GetCode(SelectedLocale));
    }

    private void ResetBooks()
    {
        Page = 1;
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
