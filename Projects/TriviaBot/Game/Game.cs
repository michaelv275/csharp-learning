using TriviaBot.Api;
using TriviaBot.Api.Models;
using TriviaBot.Common.Utilities;
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

            OpenTriviaResponse? triviaQuestions = await GetTriviaQuestions(requestClient);

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
                string escapedQuestion = System.Net.WebUtility.HtmlDecode(question.Question);
                Console.WriteLine($"Question: {escapedQuestion}");
                List<string> allAnswers = new List<string>(question.IncorrectAnswers)
                {
                    question.CorrectAnswer,
                }.OrderBy(a => Guid.NewGuid()).ToList(); // Shuffle answers

                for (int i = 0; i < allAnswers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {System.Net.WebUtility.HtmlDecode(allAnswers[i])}");
                }

                Dictionary<Player, string> playerAnswers = GetPlayerAnswers(players, allAnswers);

                DisplayAnswerResults(playerAnswers, question.CorrectAnswer);
            }

            Console.WriteLine("\nFinal Scores:");
            foreach (Player player in players)
            {
                Console.WriteLine($"{player.Name}: {player.Score} correct answer(s), {player.NumIncorrectAnswers} incorrect answer(s)");
            }
        }

        private static Dictionary<Player, string> GetPlayerAnswers(List<Player> players, List<string> allAnswers)
        {
            Dictionary<Player, string> playerAnswers = [];
            foreach (Player player in players)
            {
                int playerAnswerIndex = GetSecureIntInputFromUser($"{player.Name}, enter the number of your answer:") - 1;

                if (playerAnswerIndex >= 0 && playerAnswerIndex < allAnswers.Count)
                {
                    playerAnswers[player] = allAnswers[playerAnswerIndex];
                }
                else
                {
                    Console.WriteLine("Invalid answer selection.");
                }
            }

            return playerAnswers;
        }

        private static void DisplayAnswerResults(Dictionary<Player, string> playerAnswers, string correctAnswer)
        {
            Console.Write($"\nThe correct answer was:");
            ConsoleUtility.WriteColored($" {correctAnswer}\n", ConsoleColor.Cyan);
            foreach (KeyValuePair<Player, string> entry in playerAnswers)
            {
                string playerName = entry.Key.Name;
                string playerAnswer = entry.Value;

                if (playerAnswer == correctAnswer)
                {
                    ConsoleUtility.WriteColoredLine($"{playerName} answered correctly!", ConsoleColor.Green);
                    entry.Key.Score++;
                }
                else
                {

                    entry.Key.NumIncorrectAnswers++;
                    ConsoleUtility.WriteColoredLine($"{playerName} answered incorrectly. Their answer: {playerAnswer}", ConsoleColor.Red);
                }
            }
        }

        private static int GetIntInputFromUser(string prompt)
        {
            ConsoleUtility.WriteColored($"\n{prompt} ", ConsoleColor.Yellow);
            bool isInputValid = int.TryParse(Console.ReadLine().Trim(), out int inputNumber);

            if (isInputValid && inputNumber > 0)
            {
                return inputNumber;
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid input. Please enter a valid number.", ConsoleColor.Red);
                return GetIntInputFromUser(prompt);
            }
        }

        private static int GetSecureIntInputFromUser(string prompt)
        {
            ConsoleUtility.WriteColored($"\n{prompt} ", ConsoleColor.Yellow);
            bool isInputValid = int.TryParse(ConsoleUtility.GetSecureUserInput(), out int inputNumber);

            if (isInputValid && inputNumber > 0)
            {
                return inputNumber;
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid input. Please enter a valid number.", ConsoleColor.Red);
                return GetSecureIntInputFromUser(prompt);
            }
        }

        private static int GetIntInputFromUser(string prompt, int defaultValue)
        {
            Console.Write($"\n{prompt}");
            bool isInputValid = int.TryParse(Console.ReadLine().Trim(), out int inputNumber);

            return isInputValid && inputNumber > 0
                ? inputNumber
                : defaultValue;
        }

        private static List<Player> CreatePlayers()
        {
            int numberOfPlayers = GetIntInputFromUser("How many players will be playing?");

            List<Player> players = [];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                ConsoleUtility.WriteColored($"\nEnter name for Player {i + 1}: ", ConsoleColor.Yellow);
                string playerName = Console.ReadLine().Trim();
                // Add player to list
                players.Add(new Player(playerName));
            }

            return players;
        }

        private static TriviaCategory GetCategoryFromUser()
        {
            List<TriviaCategory> categories = OpenTriviaApi.GetCategories();

            Console.Write("Select a category by entering the corresponding number:");
            List<int> categoryIndices = [];

            foreach (TriviaCategory category in categories)
            {
                Console.WriteLine($"{(int)category}: {category}");
                categoryIndices.Add((int)category);
            }

            int selectedNumber = GetIntInputFromUser("Enter category number (Default is General Knowledge):", (int)TriviaCategory.GeneralKnowledge);

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

        private static OpenTriviaQuestionDifficulty GetDifficultyFromUser()
        {
            List<OpenTriviaQuestionDifficulty> difficulties = EnumUtility.GetValues<OpenTriviaQuestionDifficulty>().ToList();

            ConsoleUtility.WriteColoredLine("Select a difficulty by entering the corresponding number:", ConsoleColor.Yellow);
            List<int> difficultyIndices = [];

            foreach (OpenTriviaQuestionDifficulty difficultyOption in difficulties)
            {
                Console.WriteLine($"{(int)difficultyOption}: {difficultyOption}");
                difficultyIndices.Add((int)difficultyOption);
            }

            int selectedNumber = GetIntInputFromUser("Enter difficulty number (Default is Easy):", (int)OpenTriviaQuestionDifficulty.Easy);

            if (difficultyIndices.Contains(selectedNumber))
            {
                OpenTriviaQuestionDifficulty userSelectedCategory = (OpenTriviaQuestionDifficulty)selectedNumber;

                return userSelectedCategory;
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid selection", ConsoleColor.Red);
                return GetDifficultyFromUser();
            }
        }

        private static async Task<OpenTriviaResponse?> GetTriviaQuestions(ApiHelper client)
        {
            int numberOfQuestions = GetIntInputFromUser("How many trivia questions would you like to answer?");
            OpenTriviaQuestionDifficulty userDifficulty = GetDifficultyFromUser();
            TriviaCategory category = GetCategoryFromUser();

            OpenTriviaResponse questionsResult;
            try
            {
                questionsResult = await OpenTriviaApi.GetQuestions(client, numberOfQuestions, category, userDifficulty);
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