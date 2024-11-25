public static class Times
{
    public static int ToInt(double n)
    {
        var random = new Random();
        System.Console.WriteLine(Math.Floor(n) + random.NextDouble() < n % 1 ? 1 : 0);
        return (int)(Math.Floor(n) + (random.NextDouble() < n % 1 ? 1 : 0));
    }
}
