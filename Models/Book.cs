using Bogus;

namespace BookStoreTester.Models;

public class Book
{
    public string ISBN { get; }

    public string Title { get; }

    public PublishInfo PublishInfo { get; }

    public double Likes { get; }

    public string ImageUrl { get; }

    public IEnumerable<Review> Reviews { get; }

    public Authors Authors { get; }

    public Book(Faker faker, double likes, double reviews)
    {
        ISBN = faker.Random.ReplaceNumbers("978-#-##-######-#");
        Title = faker.Commerce.Product();
        Authors = new Authors(faker);
        PublishInfo = new PublishInfo(faker);
        Likes = generateInt(likes);
        Reviews = Review.GenerateReviews(generateInt(reviews), faker);
        ImageUrl = faker.Image.LoremFlickrUrl(200, 300, "cover");
    }

    private int generateInt(double forOne)
    {
        // TODO: better and another class
        return (int)Math.Floor(forOne);
    }

    public static IEnumerable<Book> GenerateBooks(
        int amount,
        Faker faker,
        double likes,
        double reviews
    )
    {
        return Enumerable.Range(1, amount).Select(_ => new Book(faker, likes, reviews));
    }
}
