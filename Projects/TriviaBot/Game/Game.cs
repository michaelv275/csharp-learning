using TriviaBot.Api;
using TriviaBot.Api.Models;
using TriviaBot.Enums;

namespace TriviaBot.Game
{
    public class TriviaGame
    {
        public void Start()
        {

        }

        public void DisplayQuestion()
        {

        }
        public int GetIntInputFromUser(string prompt)
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

        public List<Player> CreatePlayers()
        {
            int numberOfPlayers = GetIntInputFromUser("How many players will be playing?");

            List<Player> players = [];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.WriteLine($"Enter name for Player {i + 1}:");
                string playerName = Console.ReadLine().Trim();
                // Add player to list
                players.Add(new Player(playerName));
            }

            return players;
        }

        public TriviaCategory GetCategoryFromUser()
        {
            List<TriviaCategory> categories = OpenTriviaApi.GetCategories();

            Console.WriteLine("Select a category by entering the corresponding number:");
            List<int> categoryIndices = [];

            foreach (TriviaCategory category in categories)
            {
                Console.WriteLine($"{(int)category}: {category}");
                categoryIndices.Add((int)category);
            }

            int selectedNumber = GetIntInputFromUser("Enter category number:");

            if (categoryIndices.Contains(selectedNumber))
            {
                return (TriviaCategory)selectedNumber;
            }
            else
            {
                Console.WriteLine("Invalid selection");
                return GetCategoryFromUser();
            }
        }

        public async Task<OpenTriviaResponse?> GetTriviaQuestions(ApiHelper client, TriviaCategory category = TriviaCategory.GeneralKnowledge)
        {
            int numberOfQuestions = GetIntInputFromUser("How many trivia questions would you like to answer?");

            OpenTriviaResponse questionsResult;
            try
            {
                questionsResult = await OpenTriviaApi.GetQuestions(client, numberOfQuestions, category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting trivia questions: {ex.Message}");
                return null;
            }

            if (questionsResult is null || questionsResult.Results.Count == 0)
            {
                Console.WriteLine("Could not get questions from API");
                return null;
            }

            return questionsResult;
        }

    }
}