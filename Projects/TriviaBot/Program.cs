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

            OpenTriviaResponse testResponse;
            try
            {
                testResponse = await OpenTriviaApi.GetQuestions(requestClient, 5);
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
        }
    }
}
