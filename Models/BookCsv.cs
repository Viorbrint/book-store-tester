using BookStoreTester.Helpers;

namespace BookStoreTester.Models;

public class BookCsv
{
    public string ISBN { get; set; }

    public string Title { get; set; }

    public string Publisher { get; set; }

    public string PublishYear { get; set; }

    public int Likes { get; set; }

    public string Authors { get; set; }

    public BookCsv(Book book)
    {
        ISBN = book.ISBN;
        Title = book.Title;
        Publisher = book.PublishInfo.Publisher;
        PublishYear = book.PublishInfo.PublishYear;
        Likes = book.Likes;
        Authors = AuthorsFormatter.Format(book.Authors);
    }
}
