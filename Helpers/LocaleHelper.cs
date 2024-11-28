namespace BookStoreTester.Helpers;

public static class LocaleHelper
{
    private static Dictionary<string, string> Locales { get; } =
        new()
        {
            ["English (US)"] = "en",
            ["French (FR)"] = "fr",
            ["Russian (RU)"] = "ru",
        };

    public static List<string> GetAvailable() => [.. Locales.Keys];

    public static string GetCode(string locale) => Locales[locale];

    public static string GetDefault() => Locales.Keys.First();
}
