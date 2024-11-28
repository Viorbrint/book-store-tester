namespace BookStoreTester.Models;

public static class WordsProvider
{
    private static (List<string> Adjectives, List<string> Nouns, List<string> Verbs) enWords = (
        new List<string>
        {
            "Brave",
            "Mysterious",
            "Silent",
            "Ancient",
            "Forgotten",
            "Golden",
            "Dark",
            "Lost",
            "Hidden",
            "Secret",
        },
        new List<string>
        {
            "Warrior",
            "City",
            "Temple",
            "Kingdom",
            "Forest",
            "Ocean",
            "Empire",
            "Treasure",
            "Shadow",
            "Legend",
        },
        new List<string>
        {
            "conquer",
            "discover",
            "explore",
            "fight",
            "unravel",
            "decipher",
            "survive",
            "defend",
            "create",
            "lead",
        }
    );

    private static (List<string> Adjectives, List<string> Nouns, List<string> Verbs) ruWords = (
        new List<string>
        {
            "Древний",
            "Тайный",
            "Забытый",
            "Мудрый",
            "Великий",
            "Могучий",
            "Зловещий",
            "Потерянный",
            "Скрытый",
            "Темный",
        },
        new List<string>
        {
            "Герой",
            "Город",
            "Храм",
            "Королевство",
            "Лес",
            "Океан",
            "Цивилизация",
            "Сокровище",
            "Империя",
            "Легенда",
        },
        new List<string>
        {
            "покорить",
            "открыть",
            "исследовать",
            "сражаться",
            "раскрыть",
            "разгадывать",
            "выжить",
            "защищать",
            "создавать",
            "вести",
        }
    );

    private static (List<string> Adjectives, List<string> Nouns, List<string> Verbs) frWords = (
        new List<string>
        {
            "Mystérieux",
            "Ancien",
            "Oublié",
            "Secret",
            "Céleste",
            "Perdu",
            "Mouvementé",
            "Fauve",
            "Sombre",
            "Légendaire",
        },
        new List<string>
        {
            "Guerrier",
            "Temple",
            "Royaume",
            "Forêt",
            "Océan",
            "Empire",
            "Trésor",
            "Légende",
            "Ville",
            "Magie",
        },
        new List<string>
        {
            "conquérir",
            "découvrir",
            "explorer",
            "combattre",
            "déchiffrer",
            "révéler",
            "survivre",
            "défendre",
            "créer",
            "diriger",
        }
    );

    public static (
        List<string> Adjectives,
        List<string> Nouns,
        List<string> Verbs
    ) GetLocalizedWords(string locale)
    {
        if (locale == "en")
        {
            return enWords;
        }
        else if (locale == "ru")
        {
            return ruWords;
        }
        else if (locale == "fr")
        {
            return frWords;
        }
        else
        {
            return (new List<string>(), new List<string>(), new List<string>());
        }
    }
}
