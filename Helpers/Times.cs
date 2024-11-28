using Bogus;

public static class Times
{
    public static int ToInt(double n)
    {
        var faker = new Faker();
        var randomDouble = faker.Random.Double(0, 1);
        return (int)(Math.Floor(n) + (randomDouble < n % 1 ? 1 : 0));
    }
}
