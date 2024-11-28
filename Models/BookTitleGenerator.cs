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
            .Replace("{Adjective}", _faker.PickRandom(localizedWords.Adjectives)) // Прилагательное
            .Replace("{Noun}", _faker.PickRandom(localizedWords.Nouns)) // Существительное
            .Replace("{Place}", _faker.Address.City()) // Место
            .Replace("{Hero}", _faker.Name.FirstName()) // Герой
            .Replace("{Action}", _faker.Lorem.Word()); // Действие

        return LocalizeTemplate(title, locale);
    }

    private string LocalizeTemplate(string template, string locale)
    {
        if (locale == "ru")
        {
            template = template
                .Replace("The", "") // Убираем "The"
                .Replace("of", "из") // Переводим "of" в "из"
                .Replace("in", "в") // Переводим "in" в "в"
                .Replace("and the", "и") // Переводим "and the" в "и"
                .Replace("Chronicles", "Хроники") // Переводим "Chronicles" в "Хроники"
                .Replace("Secrets", "Секреты") // Переводим "Secrets" в "Секреты"
                .Replace("Tales", "Сказки"); // Переводим "Tales" в "Сказки"
        }
        else if (locale == "fr")
        {
            template = template
                .Replace("The", "Le") // Переводим "The" как "Le"
                .Replace("of", "de") // Переводим "of" в "de"
                .Replace("in", "dans") // Переводим "in" в "dans"
                .Replace("and the", "et le") // Переводим "and the" в "et le"
                .Replace("Chronicles", "Chroniques") // Переводим "Chronicles" в "Chroniques"
                .Replace("Secrets", "Secrets") // "Secrets" в "Secrets"
                .Replace("Tales", "Contes"); // "Tales" в "Contes"
        }

        return template;
    }
}
