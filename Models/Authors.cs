using Bogus;

namespace BookStoreTester.Models;

public class Authors
{
    public IEnumerable<string> Names { get; }

    public Authors(Faker faker)
    {
        var number = faker.Random.Int(1, 2);

        Names = Enumerable.Range(1, number).Select(_ => faker.Name.FullName());
    }
}
