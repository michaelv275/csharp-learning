using TriviaBot.Api;
using TriviaBot.Game;

namespace TriviaBot
{
    internal class Program
    {
        private static async Task Main(string[] _1)
        {
            ApiHelper requestClient = new ApiHelper("https://opentdb.com/api.php");
            TriviaGame triviaGame = new TriviaGame();

            await triviaGame.Start(requestClient);
        }
    }
}
