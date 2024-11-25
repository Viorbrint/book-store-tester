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

    private Faker Faker { get; set; }

    public Book(Faker faker, int likes, int reviews)
    {
        Faker = faker;
        System.Console.WriteLine(faker.IndexFaker);
        System.Console.WriteLine(likes);
        System.Console.WriteLine(reviews);
        ISBN = faker.Random.ReplaceNumbers("978-#-##-######-#");
        Title = faker.Commerce.Product();
        Authors = new Authors(faker);
        PublishInfo = new PublishInfo(faker);
        Likes = likes;
        ImageUrl = faker.Image.LoremFlickrUrl(200, 300, "cover");
        setReviews(reviews);
    }

    public void setReviews(int amount)
    {
        Reviews = Review.GenerateReviews(amount, Faker).ToList();
    }

    public static IEnumerable<Book> GenerateBooks(int amount, Faker faker, int likes, int reviews)
    {
        System.Console.WriteLine("GenerateBooks");
        return Enumerable.Range(1, amount).Select(_ => new Book(faker, likes, reviews));
    }
}
