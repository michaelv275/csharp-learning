using TriviaBot.Api;
using TriviaBot.Api.Models;
using TriviaBot.Enums;

namespace TriviaBot.Game
{
    public class TriviaGame
    {
        /**
        * Starts the trivia game by getting user preferences and fetching questions.
        */
        public async Task Start(ApiHelper requestClient)
        {
            List<Player> players = CreatePlayers();

            TriviaCategory questionCategory = GetCategoryFromUser();

            OpenTriviaResponse? triviaQuestions = await GetTriviaQuestions(requestClient, questionCategory);

            if (triviaQuestions is null)
            {
                Console.WriteLine("No trivia questions found. Exiting application.");
                return;
            }

            DisplayQuestions(triviaQuestions.Results, players);
        }

        public void DisplayQuestions(List<TriviaQuestion> questions, List<Player> players)
        {
            foreach (TriviaQuestion question in questions)
            {
                Console.WriteLine($"Question: {question.Question}");
                List<string> allAnswers = new List<string>(question.IncorrectAnswers)
                {
                    question.CorrectAnswer
                }.OrderBy(a => Guid.NewGuid()).ToList(); // Shuffle answers

                for (int i = 0; i < allAnswers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {allAnswers[i]}");
                }

                Dictionary<string, string> playerAnswers = GetPlayerAnswers(players, allAnswers);


                DisplayAnswerResults(playerAnswers, question.CorrectAnswer);
            }
        }

        private static Dictionary<string, string> GetPlayerAnswers(List<Player> players, List<string> allAnswers)
        {
            Dictionary<string, string> playerAnswers = new Dictionary<string, string>();
            foreach (Player player in players)
            {
                int playerAnswerIndex = GetIntInputFromUser($"{player.Name}, enter the number of your answer:") - 1;

                if (playerAnswerIndex >= 0 && playerAnswerIndex < allAnswers.Count)
                {
                    playerAnswers[player.Name] = allAnswers[playerAnswerIndex];
                }
                else
                {
                    Console.WriteLine("Invalid answer selection.");
                }
            }

            return playerAnswers;
        }

        private static void DisplayAnswerResults(Dictionary<string, string> playerAnswers, string correctAnswer)
        {
            Console.WriteLine($"The correct answer was: {correctAnswer}");
            foreach (var entry in playerAnswers)
            {
                string playerName = entry.Key;
                string playerAnswer = entry.Value;

                if (playerAnswer == correctAnswer)
                {
                    Console.WriteLine($"{playerName} answered correctly!");
                }
                else
                {
                    Console.WriteLine($"{playerName} answered incorrectly. Their answer: {playerAnswer}");
                }
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

        private static TriviaCategory GetCategoryFromUser()
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
                TriviaCategory userSelectedCategory = (TriviaCategory)selectedNumber;

                if (userSelectedCategory == TriviaCategory.RandomCategory)
                {
                    Random rand = new Random();
                    int randomIndex = rand.Next(categories.Count);
                    userSelectedCategory = categories[randomIndex];
                }

                return userSelectedCategory;
            }
            else
            {
                Console.WriteLine("Invalid selection");
                return GetCategoryFromUser();
            }
        }

        private static async Task<OpenTriviaResponse?> GetTriviaQuestions(ApiHelper client, TriviaCategory category = TriviaCategory.GeneralKnowledge)
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