using System.Text.Json;
using Bogus;

public class ImageService
{
    private const string FilePath = "Images/allimages.json";

    public List<string> GetImageUrls()
    {
        var json = File.ReadAllText(FilePath);
        var urls = JsonSerializer.Deserialize<List<string>>(json);
        if (urls == null)
        {
            throw new Exception($"Error reading {FilePath}");
        }
        return urls;
    }

    private List<string> Urls { get; }

    public string GenerateUrl(Faker faker)
    {
        return Urls[faker.Random.Int(0, Urls.Count - 1)];
    }

    public ImageService()
    {
        Urls = GetImageUrls();
    }
}
