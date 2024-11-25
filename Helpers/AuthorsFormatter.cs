using BookStoreTester.Models;

namespace BookStoreTester.Helpers;

public static class AuthorsFormatter
{
    public static string Format(Authors authors) => string.Join(", ", authors.Names);
}
