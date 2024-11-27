using BookStoreTester.Models;

namespace BookStoreTester.Helpers;

public static class ReviewFormatter
{
    public static string FormatAuthor(ReviewDto review)
    {
        return $"{review.Author}, {review.Company}";
    }
}
