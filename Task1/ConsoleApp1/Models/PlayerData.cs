public class PlayerData
{
    private string playerName;

    public string PlayerName
    {
        get { return playerName; }
        set
        {
            if (!Utility.EmptyInput(value))
                playerName = value;
            else
                throw new Exception("You cannot enter empty player's name");
        }
    }

    public string[] PlayerWords { get; set; }
    public int PlayerNumber { get; set; }
    public int ErrorAttempts { get; set; }


    // constructor
    private PlayerData(int num, string name)
    {
        PlayerNumber = num;
        PlayerName = name;
        PlayerWords = [];
        ErrorAttempts = 0;
    }

    public static PlayerData CreatePlayerData(int num)
    {
        Console.Write($"\nEnter name for player #{num}: ");
        string name = Console.ReadLine() ?? "";
        return new PlayerData(num, name);
    }

    public void ShowPlayerData()
    {
        Console.Write($"\nPlayer #1: ${PlayerName}");
        Console.Write($"\nPlayer #1 used words for the last game: ${PlayerWords}");
    }
}

