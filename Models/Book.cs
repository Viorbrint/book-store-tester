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

    public Authors Authors { get; }

    private Faker Faker { get; } = new Faker();

    public Book(int likes, int reviews)
    {
        System.Console.WriteLine(Faker.IndexFaker);
        System.Console.WriteLine(likes);
        System.Console.WriteLine(reviews);
        ISBN = Faker.Random.ReplaceNumbers("978-#-##-######-#");
        Title = Faker.Commerce.Product();
        Authors = new Authors(Faker);
        PublishInfo = new PublishInfo(Faker);
        Likes = likes;
        ImageUrl = Faker.Image.LoremFlickrUrl(200, 300, "cover");
        setReviews(reviews);
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
