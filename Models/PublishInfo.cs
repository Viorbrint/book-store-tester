using Bogus;

namespace BookStoreTester.Models;

public class PublishInfo
{
    public string Publisher { get; }
    public string PublishYear { get; }

    public PublishInfo(Faker faker)
    {
        Publisher = faker.Company.CompanyName();
        PublishYear = faker
            .Date.BetweenDateOnly(new DateOnly(2000, 1, 1), DateOnly.FromDateTime(DateTime.Now))
            .ToString("yyyy");
    }

    public PublishInfoDto toDto()
    {
        return new PublishInfoDto { Publisher = this.Publisher, PublishYear = this.PublishYear };
    }
}
