using Bogus;

namespace BookStoreTester.Models;

public class ReviewTextGenerator
{
    private readonly Faker _faker;

    public ReviewTextGenerator(Faker faker)
    {
        _faker = faker;
    }

    public string GenerateReviewText()
    {
        var locale = _faker.Locale;
        var reviewTemplates = GetReviewTemplates(locale);
        var template = _faker.PickRandom(reviewTemplates);
        var localizedWords = WordsProvider.GetLocalizedWords(locale);

        var words = WordsProvider.GetLocalizedWords(locale);

        return template
            .Replace("{Adjective}", _faker.PickRandom(localizedWords.Adjectives)) // Прилагательное
            .Replace("{Noun}", _faker.PickRandom(localizedWords.Nouns)) // Существительное
            .Replace("{Verb}", _faker.PickRandom(localizedWords.Verbs)) // Глагол
            .Replace("{Place}", _faker.Address.City()) // Место
            .Replace("{Hero}", _faker.Name.FirstName()); // Герой
    }

    private List<string> GetReviewTemplates(string locale)
    {
        if (locale == "en")
        {
            return new List<string>
            {
                "The {Adjective} {Noun} at {Place} is truly {Verb}. Highly recommend it!",
                "I loved the {Adjective} {Noun}, it made me feel like {Verb} at {Place}. A must-read!",
                "This book about {Place} is {Adjective} and {Verb}. {Hero} did an amazing job!",
                "The {Adjective} {Noun} in {Place} was {Verb} and unforgettable.",
                "I couldn't put down the {Adjective} {Noun} in {Place}, it was {Verb}!",
                "The story of {Place} with {Adjective} {Noun} is truly {Verb}. A masterpiece!",
                "If you love {Adjective} {Noun} and adventures in {Place}, this book is for you.",
                "The {Adjective} {Noun} at {Place} changed my life. {Hero} is an amazing author!",
                "This {Adjective} {Noun} set in {Place} is {Verb} and captivating. Don't miss it!",
                "I highly recommend this {Adjective} {Noun}. {Place} was the perfect setting for it.",
            };
        }
        else if (locale == "ru")
        {
            return new List<string>
            {
                "Очень {Adjective} {Noun} в {Place}. Настоятельно рекомендую!",
                "Мне очень понравился {Adjective} {Noun}, он заставил меня {Verb} в {Place}. Обязательно читайте!",
                "Эта книга о {Place} — это {Adjective} и {Verb}. {Hero} потрясающе справился с задачей!",
                "Захватывающий {Adjective} {Noun} в {Place}. Удивительный рассказ!",
                "Невероятная книга о {Place}. {Adjective} {Noun} и {Verb}. Читайте обязательно!",
                "Если вы любите {Adjective} {Noun}, то эта книга о {Place} вам точно понравится.",
                "Давно не читал ничего такого {Adjective} и {Verb}, книга о {Place} — шедевр!",
                "Очень {Adjective} {Noun} в {Place}, который не оставляет равнодушным. {Hero} потрясающий автор!",
                "Книга о {Place} — это {Adjective} и {Verb}, идеальный подарок для любителей {Noun}.",
                "Не могу не порекомендовать {Adjective} {Noun}. Книга о {Place} просто фантастична!",
            };
        }
        else if (locale == "fr")
        {
            return new List<string>
            {
                "Le {Adjective} {Noun} à {Place} est vraiment {Verb}. Je le recommande vivement !",
                "J'ai adoré le {Adjective} {Noun}, cela m'a donné envie de {Verb} à {Place}. À lire absolument !",
                "Ce livre sur {Place} est {Adjective} et {Verb}. {Hero} a fait un travail incroyable !",
                "Un {Adjective} {Noun} dans {Place}, c'est tout simplement {Verb}. À ne pas manquer !",
                "Ce livre est un chef-d'œuvre avec un {Adjective} {Noun} à {Place}, à lire absolument.",
                "Si vous aimez les {Adjective} {Noun} et les aventures à {Place}, ce livre est fait pour vous.",
                "L'histoire du {Adjective} {Noun} à {Place} est {Verb} et pleine de rebondissements.",
                "Le {Adjective} {Noun} à {Place} m'a captivé. Un livre {Verb} que je recommande !",
                "Ce {Adjective} {Noun} dans {Place} m'a fait {Verb}, une expérience incroyable.",
                "Un {Adjective} {Noun} à {Place}, c'est tout simplement {Verb}. Le livre de l'année!",
            };
        }
        else
        {
            return new List<string> { "Unknown language review template." };
        }
    }
}
