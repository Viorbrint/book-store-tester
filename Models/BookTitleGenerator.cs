using Bogus;

namespace BookStoreTester.Models;

public class BookTitleGenerator
{
    private readonly Faker _faker;

    public BookTitleGenerator(Faker faker)
    {
        _faker = faker;
    }

    public string GenerateBookTitle(string locale)
    {
        var templates = new List<string>
        {
            "{Adjective} {Noun}",
            "{Noun} of {Place}",
            "{Adjective} {Noun} and the {Noun}",
            "The {Noun} Chronicles",
            "The Secrets of {Place}",
            "The {Adjective} {Noun} of {Hero}",
            "{Adjective} {Noun} Tales",
            "{Adjective} {Noun} in {Place}",
            "{Noun} and the {Noun} of {Place}",
        };

        var template = _faker.PickRandom(templates);
        var localizedWords = WordsProvider.GetLocalizedWords(locale);

        string title = template
            .Replace("{Adjective}", _faker.PickRandom(localizedWords.Adjectives))
            .Replace("{Noun}", _faker.PickRandom(localizedWords.Nouns))
            .Replace("{Place}", _faker.Address.City())
            .Replace("{Hero}", _faker.Name.FirstName())
            .Replace("{Action}", _faker.Lorem.Word());

        return LocalizeTemplate(title, locale);
    }

    private string LocalizeTemplate(string template, string locale)
    {
        if (locale == "ru")
        {
            template = template
                .Replace("The", "")
                .Replace("of", "из")
                .Replace("in", "в")
                .Replace("and the", "и")
                .Replace("Chronicles", "Хроники")
                .Replace("Secrets", "Секреты")
                .Replace("Tales", "Сказки");
        }
        else if (locale == "fr")
        {
            template = template
                .Replace("The", "Le")
                .Replace("of", "de")
                .Replace("in", "dans")
                .Replace("and the", "et le")
                .Replace("Chronicles", "Chroniques")
                .Replace("Secrets", "Secrets")
                .Replace("Tales", "Contes");
        }

        return template;
    }
}
