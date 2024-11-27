using BookStoreTester.Models;

namespace BookStoreTester.Helpers;

public static class PublishInfoFormatter
{
    public static string Format(PublishInfoDto publishInfo)
    {
        return $"{publishInfo.Publisher}, {publishInfo.PublishYear}";
    }
}
