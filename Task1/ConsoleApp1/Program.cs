using MyProject.Services;

namespace MyProject
{
    class Program
    {

        static async Task Main()
        {
            const int MaxNumberOfErrorAttempts = 3;
            GameService gameService = new GameService(MaxNumberOfErrorAttempts);
            string fileName = @"/gameResults.txt";

            await gameService.StartGame(fileName);
        }
    }
}