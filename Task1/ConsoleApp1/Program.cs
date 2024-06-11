using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;


class Program
{
    const int MaxNumberOfErrorAttempts = 3;

    static async Task Main()
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string fileName = @"/gameResults.txt";
        string path = currentDirectory + fileName;
        FileInfo fileInfo = new FileInfo(path);
        Dictionary<string, int> peopleDictionary = new Dictionary<string, int>();


        if (fileInfo.Exists)
        {
            Console.WriteLine($"There is a file with data: {Path.GetFileName(path)}");
            // file reading
            string fileText = await File.ReadAllTextAsync(path);
            peopleDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(fileText);
            Console.WriteLine(fileText);
        }
        else
        {
            using (var stream = File.Create(path))
            {
                // close after creation
            }
        }

        GameData gameData = new GameData();

        try
        {
            StartGame(gameData, peopleDictionary);
        }
        catch (Exception ex)
        {
            Utility.StyledMessage(ex.ToString(), Utility.MessageType.ERROR);

            if (peopleDictionary.ContainsKey(gameData.PlayerFirst.PlayerName))
                peopleDictionary[gameData.PlayerFirst.PlayerName] = gameData.Winner == gameData.PlayerFirst.PlayerName
                    ? ++peopleDictionary[gameData.PlayerFirst.PlayerName]
                    : peopleDictionary[gameData.PlayerFirst.PlayerName];
            else
                peopleDictionary.Add(gameData.PlayerFirst.PlayerName, gameData.Winner == gameData.PlayerFirst.PlayerName ? 1 : 0);

            if (peopleDictionary.ContainsKey(gameData.PlayerSecond.PlayerName))
                peopleDictionary[gameData.PlayerSecond.PlayerName] = gameData.Winner == gameData.PlayerSecond.PlayerName
                   ? ++peopleDictionary[gameData.PlayerSecond.PlayerName]
                   : peopleDictionary[gameData.PlayerSecond.PlayerName];
            else
                peopleDictionary.Add(gameData.PlayerSecond.PlayerName, gameData.Winner == gameData.PlayerSecond.PlayerName ? 1 : 0);


            string json = JsonConvert.SerializeObject(peopleDictionary, Formatting.Indented);
            // write data into the file
            await File.WriteAllTextAsync(path, json);
            // // дозапись в конец файла
            // await File.AppendAllTextAsync(path, "\nHello work");

            // чтение файла
            string fileText = await File.ReadAllTextAsync(path);
            Console.WriteLine(fileText);
        }
    }
    static void StartGame(GameData gameData, Dictionary<string, int> peopleDictionary)
    {
        Console.Write("Enter initial word: ");
        string initialInput = Console.ReadLine() ?? "";

        if (!CheckInitialInput(initialInput)) return;

        int wordNumber = 0;
        // int attempts = 0;
        // const int maxNumberOfAttempts = 3;
        List<string> usedWords = new List<string> { initialInput };

        while (gameData.PlayerFirst.ErrorAttempts < MaxNumberOfErrorAttempts && gameData.PlayerSecond.ErrorAttempts < MaxNumberOfErrorAttempts)
        {
            int attemptNumber = ++wordNumber;

            EnterWord(initialInput, gameData.PlayerFirst, usedWords, attemptNumber, peopleDictionary, gameData.PlayerSecond.PlayerName, gameData);
            EnterWord(initialInput, gameData.PlayerSecond, usedWords, attemptNumber, peopleDictionary, gameData.PlayerFirst.PlayerName, gameData);
        }

        // string errorMessage = $"You entered incorrect words the maximum number of times ({MaxNumberOfAttempts} times). The end.";
        // Utility.ThrowCustomException(errorMessage);
    }

    static void ReadFileScore()
    {

    }

    static bool EnterWord(string initialInput, PlayerData player, List<string> usedWords, int attemptNumber, Dictionary<string, int> peopleDictionary, string anotherPlayer, GameData gameData)
    {
        if (player.ErrorAttempts >= MaxNumberOfErrorAttempts)
        {
            string errorMessage = $"Player ${player.PlayerName} entered incorrect words the maximum number of times ({player.ErrorAttempts} times). The end.";
            gameData.Winner = anotherPlayer;
            Utility.ThrowCustomException(errorMessage);
        }

        Console.Write($"\nUser ${player.PlayerName} Enter word number {attemptNumber} to compare: ");
        string newInput = Console.ReadLine() ?? "";
        string listOfUsedWords = String.Join(", ", usedWords);

        if (Utility.EmptyInput(newInput))
        {
            player.ErrorAttempts++;
            EnterWord(initialInput, player, usedWords, attemptNumber, peopleDictionary, anotherPlayer, gameData);
        }
        else if (Commands.CommandsList().Contains(newInput))
        {
            switch (newInput)
            {
                case Commands.SCORE:
                    string scoreCurrentPlayers = $"Player ${player.PlayerName} won $ {peopleDictionary[player.PlayerName]} times, and Player ${anotherPlayer} won $ {peopleDictionary[anotherPlayer]} times";
                    Utility.StyledMessage(scoreCurrentPlayers, Utility.MessageType.INFO);
                    break;
                case Commands.SHOW_WORDS:
                    Utility.StyledMessage(listOfUsedWords, Utility.MessageType.INFO);
                    break;
                case Commands.TOTAL_SCORE:
                    string totalScoreCurrentPlayers = "";
                    foreach (KeyValuePair<string, int> kvp in peopleDictionary)
                    {
                        totalScoreCurrentPlayers += $"Player {kvp.Key} won {kvp.Value} times; \n";
                    }

                    if (!peopleDictionary.ContainsKey(player.PlayerName))
                        totalScoreCurrentPlayers += $"Player {player.PlayerName} won 0 times; \n";

                    if (!peopleDictionary.ContainsKey(anotherPlayer))
                        totalScoreCurrentPlayers += $"Player {anotherPlayer} won 0 times; \n";

                    Utility.StyledMessage(totalScoreCurrentPlayers, Utility.MessageType.INFO);
                    break;
                default:
                    break;
            }

            EnterWord(initialInput, player, usedWords, attemptNumber, peopleDictionary, anotherPlayer, gameData);
        }
        // if the word has been used
        else if (usedWords.Any((word) => word == newInput))
        {
            Utility.StyledMessage($"You cannot use the same word twice! \n\n Repeated word: {newInput} \n List of all words: {listOfUsedWords}");
            player.ErrorAttempts++;
            EnterWord(initialInput, player, usedWords, attemptNumber, peopleDictionary, anotherPlayer, gameData);
        }
        else
        {
            List<KeyValuePair<char, int>> countedInitial = CountChars(initialInput);
            List<KeyValuePair<char, int>> countedAnother = CountChars(newInput);

            // check the letters
            if (!IsMatch(initialInput, newInput))
            {
                player.ErrorAttempts++;
                EnterWord(initialInput, player, usedWords, attemptNumber, peopleDictionary, anotherPlayer, gameData);
            }
            else
            {
                usedWords.Add(newInput);
            }
        }
        return true;
    }

    static bool CheckInitialInput(string input)
    {
        // empty
        if (Utility.EmptyInput(input))
        {
            return false;
        }

        // check length
        if (input.Length < 8 || input.Length > 30)
        {
            Utility.StyledMessage("Your word must contain min 8 and max 30 letters!");
            return false;
        }

        // only letters
        if (!Regex.IsMatch(input, @"^[a-zA-Z]+$"))
        {
            Utility.StyledMessage("You can use only letters!");
            return false;
        }

        return true;
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
                    Utility.StyledMessage($"\nInitial word contains {anotherKey} letter {initialValueForLetter} times, when a new word contains {anotherKeyValue.Value}");
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
