using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

public class GameData
{
    private PlayerData playerFirst;

    private PlayerData playerSecond;
    private string winner;

    public PlayerData PlayerFirst
    {
        get { return playerFirst; }
        set
        {
            playerFirst = value;
        }
    }
    public PlayerData PlayerSecond
    {
        get { return playerSecond; }
        set
        {
            if (value.PlayerName == PlayerFirst?.PlayerName)
                throw new Exception("Names must be different");
            else
                playerSecond = value;
        }
    }
    public string Winner
    {
        get { return winner; }
        set
        {
            List<string> listOfAcceptableValues = new List<string> { "", PlayerFirst.PlayerName, PlayerSecond.PlayerName };
            if (!listOfAcceptableValues.Contains(value))
                throw new Exception("There is no such player");
            else
                winner = value;
        }
    }

    // constructor
    public GameData()
    {
        PlayerFirst = PlayerData.CreatePlayerData(1);
        PlayerSecond = PlayerData.CreatePlayerData(2);
        Winner = "";
    }

    public void ShowWords()
    {
        PlayerFirst.ShowPlayerData();
        PlayerSecond.ShowPlayerData();
    }
}

