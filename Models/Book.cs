using Bogus;

namespace BookStoreTester.Models;

public class Book
{
    public string ISBN { get; }

    public string Title { get; }

    public PublishInfo PublishInfo { get; }

    public int Likes { get; set; }

    public string ImageUrl { get; }

    public List<Review> Reviews { get; private set; }

    public List<string> Authors { get; }

    private Faker Faker { get; } = new Faker();

    public Book(int likes, int reviews)
    {
        ISBN = Faker.Random.ReplaceNumbers("978-#-##-######-#");
        Title = Faker.Commerce.Product();
        Authors = Enumerable
            .Range(1, Faker.Random.Int(1, 2))
            .Select(_ => Faker.Name.FullName())
            .ToList();
        PublishInfo = new PublishInfo(Faker);
        Likes = likes;
        ImageUrl = Faker.Image.LoremFlickrUrl(200, 300, "cover");
        Reviews = Review.GenerateReviews(reviews).ToList();
    }

    public BookDto ToDto()
    {
        return new BookDto
        {
            ISBN = this.ISBN,
            Title = this.Title,
            ImageUrl = this.ImageUrl,
            Authors = this.Authors,
            PublishInfo = this.PublishInfo.toDto(),
            Likes = this.Likes,
            Reviews = this.Reviews.Select(r => r.ToDto()).ToList(),
        };
    }

    public void setReviews(int amount)
    {
        Reviews = Review.GenerateReviews(amount).ToList();
    }

    public static IEnumerable<Book> GenerateBooks(int amount, int likes, int reviews)
    {
        System.Console.WriteLine("GenerateBooks");
        return Enumerable.Range(1, amount).Select(_ => new Book(likes, reviews));
    }
}
