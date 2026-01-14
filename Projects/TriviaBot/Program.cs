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

            TriviaCategory questionCategory = triviaGame.GetCategoryFromUser();

            OpenTriviaResponse? triviaQuestions = await triviaGame.GetTriviaQuestions(requestClient, questionCategory);

            if (triviaQuestions is null)
            {
                Console.WriteLine("No trivia questions found. Exiting application.");
                return;
            }
        }
    }
}
