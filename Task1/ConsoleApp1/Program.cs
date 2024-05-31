using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.Write("Enter initial word: ");
        string initialInput = Console.ReadLine() ?? "";

        if (!CheckInput(initialInput)) return;

        Console.Write("Enter another one to compare: ");
        string anotherInput = Console.ReadLine() ?? "";

        if (!CheckInput(anotherInput)) return;

        List<KeyValuePair<char, int>> countedInitial = CountChars(initialInput);
        List<KeyValuePair<char, int>> countedAnother = CountChars(anotherInput);

        int attempts = 1;
        while (IsMatch(countedInitial, countedAnother))
        {
            Console.Write("Enter word number {0} to compare: ", ++attempts);
            anotherInput = Console.ReadLine() ?? "";
            if (!CheckInput(anotherInput)) return;
            countedAnother = CountChars(anotherInput);
        }

        StyledMessage("Incorrect input: letters do not match");
        return;
    }

    static bool CheckInput(string input)
    {
        // empty
        if (input.Length == 0)
        {
            StyledMessage("You cannot enter an empty word!");
            return false;
        }

        // check length
        if (input.Length < 8 || input.Length > 30)
        {
            StyledMessage("Your word must contain min 8 and max 30 letters!");
            return false;
        }

        // only letters
        if (!Regex.IsMatch(input, @"^[a-zA-Z]+$"))
        {
            StyledMessage("You can use only letters!");
            return false;
        }

        return true;
    }


    static void StyledMessage(string message)
    {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static bool IsMatch(List<KeyValuePair<char, int>> countedInitial, List<KeyValuePair<char, int>> countedAnother)
    {
        return countedAnother.All((anotherKeyValue) =>
        {
            KeyValuePair<char, int> initialForLetter = countedInitial.Find(initialPair => initialPair.Key == anotherKeyValue.Key);
            char? anotherKey= anotherKeyValue.Key;
            int? initialValueForLetter = initialForLetter.Value;

            if (initialValueForLetter == null || initialValueForLetter < anotherKeyValue.Value)
            {
                StyledMessage($"\nInitial word contains {anotherKey} letter {initialValueForLetter} times, when a new word contains {anotherKeyValue.Value}");
                return false;
            }

            return true;
        });
    }

    static List<KeyValuePair<char, int>> CountChars(string input)
    {
        List<char> chars = input
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        IEnumerable<KeyValuePair<char, int>> result = chars
            .Select(c => new KeyValuePair<char, int>(c, input.Count(ch => ch == c)));

        Console.WriteLine($"\nCheck word: {input}");
        foreach (KeyValuePair<char, int> person in result)
        {
            Console.WriteLine($"{person}");
        }

        return result.ToList();
    }
}
