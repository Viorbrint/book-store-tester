using Bogus;

namespace BookStoreTester.Models
{
    public class Review
    {
        public string Author { get; }

        public string Text { get; }

        public string Company { get; }

        private Faker Faker { get; } = new Faker();

        public Review()
        {
            Author = Faker.Random.ReplaceNumbers(Faker.Name.FullName());
            Text = Faker.Lorem.Sentence();
            Company = Faker.Company.CompanyName();
        }

        public static IEnumerable<Review> GenerateReviews(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Review());
        }
    }
}
