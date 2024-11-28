using Bogus;

namespace BookStoreTester.Models;

public class Review
{
    public string Author { get; }

    public string Text { get; }

    public string Company { get; }

    public Review(Faker faker)
    {
        Author = faker.Random.ReplaceNumbers(faker.Name.FullName());
        var textGenerator = new ReviewTextGenerator(faker);
        Text = textGenerator.GenerateReviewText();
        Company = faker.Company.CompanyName();
    }

    public static IEnumerable<Review> GenerateReviews(int amount, Faker faker)
    {
        return Enumerable.Range(1, amount).Select(_ => new Review(faker));
    }
}
