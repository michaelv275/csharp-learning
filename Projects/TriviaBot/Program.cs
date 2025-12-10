using TriviaBot.Api;
using TriviaBot.Api.Models;
namespace TriviaBot
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ApiHelper requestClient = new ApiHelper("https://opentdb.com/api.php");

            OpenTriviaResponse triviaQuestions = GetTriviaQuestions(requestClient);
        }

        private static OpenTriviaResponse GetTriviaQuestions(ApiHelper client)
        {
            Console.WriteLine("How many trivia questions would you like to answer?");
            string numberOfQuestions = Console.ReadLine();

            while (!int.TryParse(numberOfQuestions, out _) || string.IsNullOrEmpty(numberOfQuestions))
            {
                Console.WriteLine("Please enter a valid number:");
                numberOfQuestions = Console.ReadLine();
            }

            OpenTriviaResponse testResponse;

            try
            {
                testResponse = await OpenTriviaApi.GetQuestions(requestClient, int.Parse(numberOfQuestions));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting trivia questions: {ex.Message}");
                return;
            }

            if (testResponse is null || testResponse.Results.Count == 0)
            {
                Console.WriteLine("Could not get questions from API");
                return;
            }

            return testResponse;
        }

    }
}
