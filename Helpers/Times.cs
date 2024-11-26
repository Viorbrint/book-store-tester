public static class Times
{
    public static int ToInt(double n)
    {
        var random = new Random();
        return (int)(Math.Floor(n) + (random.NextDouble() < n % 1 ? 1 : 0));
    }
}
