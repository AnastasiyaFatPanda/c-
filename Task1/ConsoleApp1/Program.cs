using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

enum MessageType
{
    ERROR,
    WARNING
}

class Program
{
    static void Main()
    {
        Console.Write("Enter initial word: ");
        string initialInput = Console.ReadLine() ?? "";

        if (!CheckInitialInput(initialInput)) return;

        int wordNumber = 0;
        int attempts = 0;
        const int maxNumberOfAttempts = 3;
        List<string> usedWords = new List<string> { initialInput };

        while (attempts < maxNumberOfAttempts)
        {
            Console.Write("\nEnter word number {0} to compare: ", ++wordNumber);
            string anotherInput = Console.ReadLine() ?? "";

            // check a new word
            if (!EmptyInput(anotherInput))
            {
                attempts++;
            }
            // if the word has been used
            else if (usedWords.Any((word) => word == anotherInput))
            {
                string listOfWords = String.Join(", ", usedWords);
                StyledMessage($"You cannot use the same word twice! \n\n Repeated word: {anotherInput} \n List of all words: {listOfWords}");
                attempts++;
            }
            else
            {
                List<KeyValuePair<char, int>> countedInitial = CountChars(initialInput);
                List<KeyValuePair<char, int>> countedAnother = CountChars(anotherInput);

                // check the letters
                if (!IsMatch(initialInput, anotherInput))
                {
                    attempts++;
                }
                else
                {
                    usedWords.Add(anotherInput);
                }
            }
        }

        StyledMessage($"You entered incorrect words the maximum number of times ({maxNumberOfAttempts} times). The end.", MessageType.ERROR);
        return;
    }

    static bool CheckInitialInput(string input)
    {
        // empty
        if (!EmptyInput(input))
        {
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

    static bool EmptyInput(string input)
    {
        // empty
        if (input.Length == 0)
        {
            StyledMessage("You cannot enter an empty word!");
            return false;
        }

        return true;
    }


    static void StyledMessage(string message, MessageType type = MessageType.WARNING)
    {
        Console.BackgroundColor = type == MessageType.ERROR ? ConsoleColor.Red : ConsoleColor.Yellow;
        Console.ForegroundColor = type == MessageType.ERROR ? ConsoleColor.White : ConsoleColor.DarkYellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static bool IsMatch(string initialInput, string anotherInput)
    {
        List<KeyValuePair<char, int>> countedInitial = CountChars(initialInput);
        List<KeyValuePair<char, int>> countedAnother = CountChars(anotherInput);

        return countedAnother.All((anotherKeyValue) =>
            {
                KeyValuePair<char, int> initialForLetter = countedInitial.Find(initialPair => initialPair.Key == anotherKeyValue.Key);
                char? anotherKey = anotherKeyValue.Key;
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

        return result.ToList();
    }
}
