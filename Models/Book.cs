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

    public Book(int likes, int reviews, Faker faker, ImageService imageService)
    {
        int minAuthors = 1;
        int maxAuthors = 2;
        ISBN = faker.Random.ReplaceNumbers("978-#-##-######-#");

        var titleGenerator = new BookTitleGenerator(faker);
        Title = titleGenerator.GenerateBookTitle(faker.Locale);

        Authors = Enumerable
            .Range(1, faker.Random.Int(minAuthors, maxAuthors))
            .Select(_ => faker.Name.FullName())
            .ToList();
        PublishInfo = new PublishInfo(faker);
        Likes = likes;
        ImageUrl = imageService.GenerateUrl(faker);
        Reviews = Review.GenerateReviews(reviews, faker).ToList();
    }

    public void SetReviews(int amount, Faker faker)
    {
        Reviews = Review.GenerateReviews(amount, faker).ToList();
    }

    public static IEnumerable<Book> GenerateBooks(
        int amount,
        int likes,
        int reviews,
        Faker faker,
        ImageService imageService
    )
    {
        return Enumerable
            .Range(1, amount)
            .Select(_ => new Book(likes, reviews, faker, imageService));
    }
}
