namespace BookStoreTester.Helpers;

public static class AuthorsFormatter
{
    public static string Format(List<string> authors) => string.Join(", ", authors);
}
