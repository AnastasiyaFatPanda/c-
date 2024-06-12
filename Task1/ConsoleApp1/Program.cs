using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
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
            Console.CancelKeyPress += (sender, args) =>
            {
                OnExit(sender, args, peopleDictionary, gameData, path);
            };
            StartGame(gameData, peopleDictionary);
        }
        catch (Exception ex)
        {
            Utility.StyledMessage(ex.ToString(), Utility.MessageType.ERROR);
            WriteScoreIntoFile(peopleDictionary, gameData, path);
        }
    }
    static void StartGame(GameData gameData, Dictionary<string, int> peopleDictionary)
    {
        Console.Write("Enter initial word: ");
        string initialInput = Console.ReadLine() ?? "";

        if (!CheckInitialInput(initialInput)) return;

        int wordNumber = 0;
        List<string> usedWords = new List<string> { initialInput };

        while (gameData.PlayerFirst.ErrorAttempts < MaxNumberOfErrorAttempts && gameData.PlayerSecond.ErrorAttempts < MaxNumberOfErrorAttempts)
        {
            int attemptNumber = ++wordNumber;

            EnterWord(initialInput, gameData.PlayerFirst, usedWords, attemptNumber, peopleDictionary, gameData.PlayerSecond.PlayerName, gameData);
            EnterWord(initialInput, gameData.PlayerSecond, usedWords, attemptNumber, peopleDictionary, gameData.PlayerFirst.PlayerName, gameData);
        }
    }

    static void OnExit(object sender, ConsoleCancelEventArgs args, Dictionary<string, int> peopleDictionary, GameData gameData, string path)
    {
        Utility.StyledMessage("The player suddenly disappeared...", Utility.MessageType.ERROR);
        WriteScoreIntoFile(peopleDictionary, gameData, path);

        // cancel closing
        // args.Cancel = true;
    }

    static async void WriteScoreIntoFile(Dictionary<string, int> peopleDictionary, GameData gameData, string path)
    {
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
        // write data at the end of the file
        // await File.AppendAllTextAsync(path, "\nHello work");

        // reading the file
        string fileText = await File.ReadAllTextAsync(path);
        Console.WriteLine(fileText);

    }

    static bool EnterWord(string initialInput, PlayerData player, List<string> usedWords, int attemptNumber, Dictionary<string, int> peopleDictionary, string anotherPlayer, GameData gameData)
    {
        gameData.Winner = anotherPlayer;
        if (player.ErrorAttempts >= MaxNumberOfErrorAttempts)
        {
            string errorMessage = $"Player {player.PlayerName} entered incorrect words the maximum number of times ({player.ErrorAttempts} times). The end.";
            Utility.ThrowCustomException(errorMessage);
        }

        Console.Write($"\nUser {player.PlayerName} Enter word number {attemptNumber} to compare: ");
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
                    int firstScore = peopleDictionary.ContainsKey(player.PlayerName) ? peopleDictionary[player.PlayerName] : 0;
                    int secondScore = peopleDictionary.ContainsKey(anotherPlayer) ? peopleDictionary[anotherPlayer] : 0;
                    string scoreCurrentPlayers =
                        $"Player {player.PlayerName} won {firstScore} times \nPlayer {anotherPlayer} won {secondScore} times";
                    Utility.StyledMessage(scoreCurrentPlayers, Utility.MessageType.INFO);
                    break;
                case Commands.SHOW_WORDS:
                    Utility.StyledMessage(listOfUsedWords, Utility.MessageType.INFO);
                    break;
                case Commands.TOTAL_SCORE:
                    string totalScoreCurrentPlayers = "Total Score:";
                    foreach (KeyValuePair<string, int> kvp in peopleDictionary)
                    {
                        totalScoreCurrentPlayers += $" \nPlayer {kvp.Key} won {kvp.Value} times;";
                    }

                    if (!peopleDictionary.ContainsKey(player.PlayerName))
                        totalScoreCurrentPlayers += $" \nPlayer {player.PlayerName} won 0 times;";

                    if (!peopleDictionary.ContainsKey(anotherPlayer))
                        totalScoreCurrentPlayers += $" \nPlayer {anotherPlayer} won 0 times;";

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
                    Utility.StyledMessage($"\nInitial word contains '{anotherKey}' letter {initialValueForLetter} times, when a new word contains {anotherKeyValue.Value}");
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
