using BookStoreTester.Models;

namespace BookStoreTester.Helpers;

public static class ReviewFormatter
{
    public static string FormatAuthor(Review review)
    {
        return $"{review.Author}, {review.Company}";
    }
}
