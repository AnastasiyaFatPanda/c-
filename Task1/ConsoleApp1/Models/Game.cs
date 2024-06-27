namespace MyProject.Models
{
    public class Game
    {
        private Player playerFirst;

        private Player playerSecond;
        private string winner;

        public Player PlayerFirst
        {
            get { return playerFirst; }
            set
            {
                playerFirst = value;
            }
        }
        public Player PlayerSecond
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
        public int CurrentPayerNumber;
        public int ItterationNumber;

        // constructor
        public Game()
        {
            PlayerFirst = Player.CreatePlayer(1);
            PlayerSecond = Player.CreatePlayer(2);
            CurrentPayerNumber = 0;
            ItterationNumber = 0;
            Winner = "";
        }

        public void ShowWords()
        {
            PlayerFirst.ShowPlayer();
            PlayerSecond.ShowPlayer();
        }

        public (Player currentPlayer, Player competitorPlayer) GetPlayersForTheNextMove()
        {
            if (CurrentPayerNumber != PlayerFirst.PlayerNumber)
            {
                CurrentPayerNumber = PlayerFirst.PlayerNumber;
                // we start each new itteration from the first gamer
                ItterationNumber++;
                return (PlayerFirst, PlayerSecond);
            }

            CurrentPayerNumber = PlayerSecond.PlayerNumber;
            return (PlayerSecond, PlayerFirst);
        }
    }
}