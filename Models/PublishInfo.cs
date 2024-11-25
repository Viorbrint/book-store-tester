using Bogus;

namespace BookStoreTester.Models;

public class PublishInfo
{
    public string Publisher { get; }
    public string PublishDate { get; }

    public PublishInfo(Faker faker)
    {
        Publisher = faker.Company.CompanyName();
        PublishDate = faker.Date.Recent().ToString("yyyy-MM-dd");
    }
}
