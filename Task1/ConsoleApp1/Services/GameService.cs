using Newtonsoft.Json;

public class GameService
{
    private readonly int _maxNumberOfErrorAttempts;

    public GameService(int maxNumberOfErrorAttempts = 10)
    {
        _maxNumberOfErrorAttempts = maxNumberOfErrorAttempts;
    }

    public async Task StartGame(string fileName)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string path = currentDirectory + fileName;
        FileInfo fileInfo = new FileInfo(path);
        Dictionary<string, int> peopleDictionary = new Dictionary<string, int>();
        Game gameData = new Game();

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
            Utility.StyledMessage("File was created");
        }

        try
        {
            Console.CancelKeyPress += (sender, args) =>
            {
                OnExit(sender, args, peopleDictionary, gameData, path);
            };
            StartWordsEntering(gameData, peopleDictionary, _maxNumberOfErrorAttempts);
        }
        catch (Exception _)
        {
            Utility.StyledMessage($"END. Winner is {gameData.Winner}", Utility.MessageType.ERROR);
            WriteScoreIntoFile(peopleDictionary, gameData, path);
        }
    }

    private void StartWordsEntering(Game gameData, Dictionary<string, int> peopleDictionary, int _maxNumberOfErrorAttempts)
    {
        Console.Write("Enter initial word: ");
        string initialInput = Console.ReadLine() ?? "";

        if (!Utility.CheckInitialInput(initialInput)) return;

        int wordNumber = 0;
        List<string> usedWords = new List<string> { initialInput };

        while (gameData.PlayerFirst.ErrorAttempts < _maxNumberOfErrorAttempts && gameData.PlayerSecond.ErrorAttempts < _maxNumberOfErrorAttempts)
        {
            int attemptNumber = ++wordNumber;

            Move(initialInput, gameData.PlayerFirst, gameData.PlayerSecond.PlayerName, gameData, usedWords, attemptNumber, peopleDictionary);
            Move(initialInput, gameData.PlayerSecond, gameData.PlayerFirst.PlayerName, gameData, usedWords, attemptNumber, peopleDictionary);
        }
    }

    private void Move(string initialInput, Player player, string playerCompetitorName, Game gameData, List<string> usedWords, int attemptNumber, Dictionary<string, int> peopleDictionary)
    {
        Utility.EnterStatus playerEnter;
        do
        {
            (playerEnter, var newInput) = EnterWord(initialInput, player, usedWords, attemptNumber, peopleDictionary, playerCompetitorName, gameData);
            switch (playerEnter)
            {
                case Utility.EnterStatus.ERROR:
                    player.ErrorAttempts++;
                    break;

                case Utility.EnterStatus.SUCCESS:
                    usedWords.Add(newInput);
                    break;

                default:
                    break;
            }
        }
        while (playerEnter != Utility.EnterStatus.SUCCESS);
    }

    private static void OnExit(object sender, ConsoleCancelEventArgs args, Dictionary<string, int> peopleDictionary, Game gameData, string path)
    {
        Utility.StyledMessage("The player suddenly disappeared...", Utility.MessageType.ERROR);
        WriteScoreIntoFile(peopleDictionary, gameData, path);
    }

   private static async void WriteScoreIntoFile(Dictionary<string, int> peopleDictionary, Game gameData, string path)
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

        // reading the file
        string fileText = await File.ReadAllTextAsync(path);
        Console.WriteLine(fileText);

    }

    private (Utility.EnterStatus status, string newWord) EnterWord(string initialInput, Player player, List<string> usedWords, int attemptNumber, Dictionary<string, int> peopleDictionary, string playerCompetitorName, Game gameData)
    {
        gameData.Winner = playerCompetitorName;

        // check if user is allowed to enter a new word
        if (player.ErrorAttempts >= _maxNumberOfErrorAttempts)
        {
            string errorMessage = $"Player {player.PlayerName} entered incorrect words the maximum number of times ({player.ErrorAttempts} times).";
            Utility.ThrowCustomException(errorMessage);

            return (Utility.EnterStatus.ERROR, "");
        }

        Console.Write($"\nUser {player.PlayerName} Enter word number {attemptNumber} to compare: ");
        string newInput = Console.ReadLine() ?? "";
        string listOfUsedWords = String.Join(", ", usedWords);

        // if a new word is empty
        if (Utility.EmptyInput(newInput))
        {
            return (Utility.EnterStatus.ERROR, "");
        }
        // if user entered a command
        else if (Commands.CommandsList().Contains(newInput))
        {
            RunCommand(newInput, peopleDictionary, player, playerCompetitorName, listOfUsedWords);
            return (Utility.EnterStatus.COMMAND, "");
        }
        // if the word has been used
        else if (usedWords.Any((word) => word == newInput))
        {
            Utility.StyledMessage($"You cannot use the same word twice! \n\n Repeated word: {newInput} \n List of all words: {listOfUsedWords}");
            return (Utility.EnterStatus.ERROR, "");
        }
        // if a new word doesn't meet all other conditions
        else if (!IsMatch(initialInput, newInput))
        {
            return (Utility.EnterStatus.ERROR, "");
        }

        // a new word is correct
        return (Utility.EnterStatus.SUCCESS, newInput);
    }

    private void RunCommand(string newInput, Dictionary<string, int> peopleDictionary, Player player, string playerCompetitorName, string listOfUsedWords)
    {
        switch (newInput)
        {
            case Commands.SCORE:
                int firstScore = peopleDictionary.ContainsKey(player.PlayerName) ? peopleDictionary[player.PlayerName] : 0;
                int secondScore = peopleDictionary.ContainsKey(playerCompetitorName) ? peopleDictionary[playerCompetitorName] : 0;
                string scoreCurrentPlayers =
                    $"Player {player.PlayerName} won {firstScore} times \nPlayer {playerCompetitorName} won {secondScore} times";
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

                if (!peopleDictionary.ContainsKey(playerCompetitorName))
                    totalScoreCurrentPlayers += $" \nPlayer {playerCompetitorName} won 0 times;";

                Utility.StyledMessage(totalScoreCurrentPlayers, Utility.MessageType.INFO);
                break;
            default:
                break;
        }
    }

    private static bool IsMatch(string initialInput, string anotherInput)
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

    private static List<KeyValuePair<char, int>> CountChars(string input)
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