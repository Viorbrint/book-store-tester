namespace BookStoreTester.Models;

public class BookDto
{
    public string ISBN { get; set; }

    public string Title { get; set; }

    public PublishInfoDto PublishInfo { get; set; }

    public int Likes { get; set; }

    public string ImageUrl { get; set; }

    public List<ReviewDto> Reviews { get; set; }

    public List<string> Authors { get; set; }
}
