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

            int cat = GetCategoryFromUser();

            OpenTriviaResponse? triviaQuestions = await GetTriviaQuestions(requestClient);

            if (triviaQuestions is null)
            {
                Console.WriteLine("No trivia questions found. Exiting application.");
                return;
            }
        }

        private static int GetIntInputFromUser(string prompt)
        {
            Console.WriteLine(prompt);
            bool isInputValid = int.TryParse(Console.ReadLine().Trim(), out int inputNumber);

            if (isInputValid && inputNumber > 0)
            {
                return inputNumber;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return GetIntInputFromUser(prompt);
            }
        }

        private static List<Player> CreatePlayers()
        {
            int numberOfPlayers = GetIntInputFromUser("How many players will be playing?");

            List<Player> players = new List<Player>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i + 1}:");
                string playerName = Console.ReadLine().Trim();
                // Add player to list
                players.Add(new Player(playerName));
            }

            return players;
        }

        private static int GetCategoryFromUser()
        {
            List<TriviaCategory> categories = OpenTriviaApi.GetCategories();

            Console.WriteLine("Select a category by entering the corresponding number:");
            List<int> categoryIndices = new List<int>();
            foreach (TriviaCategory category in categories)
            {
                Console.WriteLine($"{(int)category}: {category}");
                categoryIndices.Add((int)category);
            }

            int selectedNumber = GetIntInputFromUser("Enter category number:");

            if (categoryIndices.Contains(selectedNumber))
            {
                return selectedNumber;
            }
            else
            {
                Console.WriteLine("Invalid selection");
                return GetCategoryFromUser();
            }
        }

        private static async Task<OpenTriviaResponse?> GetTriviaQuestions(ApiHelper client)
        {
            int numberOfQuestions = GetIntInputFromUser("How many trivia questions would you like to answer?");

            OpenTriviaResponse testResponse;
            try
            {
                testResponse = await OpenTriviaApi.GetQuestions(client, numberOfQuestions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting trivia questions: {ex.Message}");
                return null;
            }

            if (testResponse is null || testResponse.Results.Count == 0)
            {
                Console.WriteLine("Could not get questions from API");
                return null;
            }

            return testResponse;
        }
    }
}
