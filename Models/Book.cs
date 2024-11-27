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

    public Book(int likes, int reviews, Faker faker)
    {
        ISBN = faker.Random.ReplaceNumbers("978-#-##-######-#");
        Title = faker.Commerce.Product();
        Authors = Enumerable
            .Range(1, faker.Random.Int(1, 2))
            .Select(_ => faker.Name.FullName())
            .ToList();
        PublishInfo = new PublishInfo(faker);
        Likes = likes;
        ImageUrl = faker.Image.LoremFlickrUrl(200, 300, "Cat");
        Reviews = Review.GenerateReviews(reviews, faker).ToList();
    }

    public void setReviews(int amount, Faker faker)
    {
        Reviews = Review.GenerateReviews(amount, faker).ToList();
    }

    public static IEnumerable<Book> GenerateBooks(int amount, int likes, int reviews, Faker faker)
    {
        return Enumerable.Range(1, amount).Select(_ => new Book(likes, reviews, faker));
    }
}
