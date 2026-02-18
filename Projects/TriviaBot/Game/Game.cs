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

            while (true)
            {
                OpenTriviaResponse? triviaQuestions = await GetTriviaQuestions(requestClient);

                if (triviaQuestions is null)
                {
                    Console.WriteLine("No trivia questions found. Exiting application.");
                    return;
                }

                DisplayQuestions(triviaQuestions.Results, players);

                if (!PlayAgainPrompt())
                {
                    Console.WriteLine("Thanks for playing!");
                    return;
                }

                string choice = NewGameOrContinueWithExistingPrompt();
                Console.Clear();

                if (choice == "new")
                {
                    players = CreatePlayers();
                }
            }
        }

        public void DisplayQuestions(List<TriviaQuestion> questions, List<Player> players)
        {
            for (int i = 0; i < questions.Count; i++)
            {
                TriviaQuestion question = questions[i];
                string escapedQuestion = System.Net.WebUtility.HtmlDecode(question.Question);

                ConsoleUtility.WriteColored($"\nQuestion {i + 1}: ", ConsoleColor.Yellow);
                Console.WriteLine($"{escapedQuestion}");

                List<string> allAnswers = new List<string>(question.IncorrectAnswers)
                {
                    question.CorrectAnswer,
                }.OrderBy(a => Guid.NewGuid()).ToList(); // Shuffle answers

                for (int j = 0; j < allAnswers.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {System.Net.WebUtility.HtmlDecode(allAnswers[j])}");
                }

                Dictionary<Player, string> playerAnswers = GetPlayerAnswers(players, allAnswers);

                ScoreQuestionAndDisplayResult(ref playerAnswers, question.CorrectAnswer);
            }

            ConsoleUtility.WriteColoredLine("\nFinal Scores:", ConsoleColor.Yellow);

            int topScore = players.Max(p => p.Score);
            int worstScore = players.Min(p => p.Score);

            foreach (Player player in players.OrderByDescending(p => p.Score).ToList())
            {
                if (player.Score == topScore)
                {
                    ConsoleUtility.WriteColoredLine($"{player.Name}: {player.Score} correct answer(s), {player.NumIncorrectAnswers} incorrect answer(s)", ConsoleColor.Green);
                }
                else if (player.Score == worstScore)
                {
                    ConsoleUtility.WriteColoredLine($"{player.Name}: {player.Score} correct answer(s), {player.NumIncorrectAnswers} incorrect answer(s)", ConsoleColor.Red);
                }
                else
                {
                    Console.WriteLine($"{player.Name}: {player.Score} correct answer(s), {player.NumIncorrectAnswers} incorrect answer(s)");
                }
            }
        }

        private static bool PlayAgainPrompt()
        {
            ConsoleUtility.WriteColored("\nWould you like to play again? (y/n): ", ConsoleColor.Yellow);
            string userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "y" || userInput == "yes")
            {
                return true;
            }
            else if (userInput == "n" || userInput == "no")
            {
                return false;
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid input. Please enter 'y' or 'n'.", ConsoleColor.Red);
                return PlayAgainPrompt();
            }
        }

        private static string NewGameOrContinueWithExistingPrompt()
        {
            ConsoleUtility.WriteColored("\nWould you like to (1) start a new game or (2) continue with the existing one? (1/2): ", ConsoleColor.Yellow);
            string userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "1")
            {
                return "new";
            }
            else if (userInput == "2")
            {
                return "continue";
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid input. Please enter '1' or '2'.", ConsoleColor.Red);
                return NewGameOrContinueWithExistingPrompt();
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

        private static void ScoreQuestionAndDisplayResult(ref Dictionary<Player, string> questionResponses, string correctAnswer)
        {
            Console.Write($"\nThe correct answer was: ");
            ConsoleUtility.WriteColored($"{System.Net.WebUtility.HtmlDecode(correctAnswer)}", ConsoleColor.Cyan);

            bool wasAnyCorrect = questionResponses.Values.Any(answer => answer == correctAnswer);
            bool wereAllCorrect = questionResponses.Values.All(answer => answer == correctAnswer);

            if (wereAllCorrect)
            {
                ConsoleUtility.WriteColoredLine("\nEveryone answered correctly! Great job!", ConsoleColor.Green);
                questionResponses.Keys.ToList().ForEach(player => player.Score++);
            }
            else if (!wasAnyCorrect)
            {
                ConsoleUtility.WriteColoredLine("\nNo one answered correctly. Bad job :(", ConsoleColor.Red);
                questionResponses.Keys.ToList().ForEach(player => player.NumIncorrectAnswers++);
            }
            else
            {
                string correctPlayers = string.Empty;

                for (int i = 0; i < questionResponses.Count; i++)
                {
                    Player responder = questionResponses.ElementAt(i).Key;
                    string playerAnswer = questionResponses.ElementAt(i).Value;

                    string playerName = responder.Name;

                    if (playerAnswer == correctAnswer)
                    {
                        if (!string.IsNullOrEmpty(correctPlayers))
                        {
                            correctPlayers += ", ";
                        }

                        correctPlayers += playerName;
                        responder.Score++;
                    }
                    else
                    {
                        responder.NumIncorrectAnswers++;
                    }
                }

                ConsoleUtility.WriteColoredLine($"\n{correctPlayers} answered correctly!", ConsoleColor.Green);
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
            ConsoleUtility.WriteColored($"\n{prompt}", ConsoleColor.Yellow);
            bool isInputValid = int.TryParse(Console.ReadLine().Trim(), out int inputNumber);

            return isInputValid && inputNumber >= 0
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

            ConsoleUtility.WriteColoredLine("Select a category by entering the corresponding number: ", ConsoleColor.Yellow);
            List<int> categoryIndices = [];

            Console.WriteLine();
            foreach (TriviaCategory category in categories)
            {
                Console.WriteLine($"{(int)category}: {category}");
                categoryIndices.Add((int)category);
            }

            int selectedNumber = GetIntInputFromUser("Enter category number (Default is General Knowledge): ", (int)TriviaCategory.GeneralKnowledge);

            Console.WriteLine();
            if (categoryIndices.Contains(selectedNumber))
            {
                TriviaCategory userSelectedCategory = (TriviaCategory)selectedNumber;

                if (userSelectedCategory == TriviaCategory.RandomCategory)
                {
                    Random rand = new Random();
                    int randomIndex = rand.Next(categories.Count);
                    userSelectedCategory = categories[randomIndex];

                    Console.Write($"Random chose: ");
                    ConsoleUtility.WriteColoredLine($"{userSelectedCategory}", ConsoleColor.Cyan);
                }

                return userSelectedCategory;
            }
            else
            {
                ConsoleUtility.WriteColoredLine("Invalid selection", ConsoleColor.Red);
                return GetCategoryFromUser();
            }
        }

        private static OpenTriviaQuestionDifficulty GetDifficultyFromUser()
        {
            List<OpenTriviaQuestionDifficulty> difficulties = EnumUtility.GetValues<OpenTriviaQuestionDifficulty>().ToList();

            ConsoleUtility.WriteColoredLine("\nSelect a difficulty by entering the corresponding number: ", ConsoleColor.Yellow);
            List<int> difficultyIndices = [];

            foreach (OpenTriviaQuestionDifficulty difficultyOption in difficulties)
            {
                Console.WriteLine($"{(int)difficultyOption}: {difficultyOption}");
                difficultyIndices.Add((int)difficultyOption);
            }

            int selectedNumber = GetIntInputFromUser("Enter difficulty number (Default is Easy): ", (int)OpenTriviaQuestionDifficulty.Easy);

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