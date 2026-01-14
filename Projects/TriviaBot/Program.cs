using TriviaBot.Api;
using TriviaBot.Api.Models;
using TriviaBot.Enums;
namespace TriviaBot
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ApiHelper requestClient = new ApiHelper("https://opentdb.com/api.php");
            Game triviaGame = new Game();

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
