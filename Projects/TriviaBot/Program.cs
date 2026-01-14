using TriviaBot.Api;
using TriviaBot.Api.Models;
using TriviaBot.Enums;
using TriviaBot.Game;

namespace TriviaBot
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ApiHelper requestClient = new ApiHelper("https://opentdb.com/api.php");
            TriviaGame triviaGame = new TriviaGame();


            List<TriviaQuestion> questions = await triviaGame.Start(requestClient);
        }
    }
}
