using MadlibBot.Enums;
using MadlibBot.Utilities;
using MadLibBot.Models;
using Madlibs.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadlibBot
{
    internal class Program
    {
        private static HttpClient _apiClient;
        private static DirectoryInfo _templateFolder;
        private static string _sourceDirectory = @"/Users/sophiapache/Documents/csharp-learning";

        static void Main(string[] _1)
        {
            _templateFolder = new DirectoryInfo($"{_sourceDirectory}/Projects/Madlibs/MadlibBot/Templates/");

            List<Player> gamePlayers = GameIntroduction();

            foreach (Player player in gamePlayers)
            {
                ConsoleUtility.WriteColored($"\nPlayer: ", ConsoleColor.Yellow);
                Console.Write(player.Name);
            }

            Console.WriteLine();

            MadLibGenres madlibGenre = GetMadlibGenre();
            ConsoleUtility.WriteColored($"You selected: ", ConsoleColor.Yellow);
            Console.Write($"{madlibGenre}");

            Console.WriteLine();

            MadLibStoryTemplate storyTemplate = GetTemplate(madlibGenre);
            Console.WriteLine($"\nThe {storyTemplate.Title} template has {storyTemplate.BlankCount} blanks");

            Dictionary<string, List<string>> examples = GetExamplesForBlankType(storyTemplate.Blanks);

            int testIndex = 0;
            foreach (MadLibBlank blank in storyTemplate.Blanks)
            {
                foreach (Player currentCaveman in gamePlayers)
                {
                    ConsoleUtility.WriteColoredLine($"caveman {currentCaveman.Name} say {blank.Type} (ex. {string.Join(", ", examples[blank.Type])})", ConsoleColor.Yellow);
                    currentCaveman.MadLibResponse.Add(ConsoleUtility.GetSecureUserInput());
                }

                testIndex++;

                if (testIndex >= 3)
                {
                    break;
                }
            }

            CreateStories(storyTemplate, gamePlayers);

            foreach (Player player in gamePlayers)
            {
                ConsoleUtility.WriteColoredLine($"{storyTemplate.Title} story for {player.Name} = ", ConsoleColor.Yellow);
                Console.WriteLine($"{player.MadlibStory}\n");
            }

            VoteForBestStory(gamePlayers);
            DetermineWinner(gamePlayers);
        }

        private static List<Player> GameIntroduction()
        {

            ConsoleUtility.WriteColoredLine("welcome 2 madlibs games i caveman.", ConsoleColor.Yellow);
            ConsoleUtility.WriteColoredLine("enter words and i make story. you like? we play? yes? no? maybe? we play anyway.", ConsoleColor.Yellow);
            ConsoleUtility.WriteColoredLine("we start now.", ConsoleColor.Yellow);

            // Ask how many human players are playing
            int playerCount = ConsoleUtility.GetPositiveIntInputFromUser("how many cavemen ooga booga");
            List<Player> playerList = [];

            for (int i = 0; i < playerCount; i++)
            {
                string cavemanName = ConsoleUtility.GetUserInput("\nwhat your name? hit enter when done new players.");
                // Validate string input, make sure not null or empty. Write function
                playerList.Add(new Player(cavemanName));
            }

            playerList.Add(new Player("BoogaBot"));

            ConsoleUtility.WriteColoredLine("\nall players added. let play", ConsoleColor.Yellow);
            return playerList;
        }

        // Get a template object for a given category
        private static MadLibStoryTemplate? GetTemplate(MadLibGenres category)
        {
            string categoryName = category.ToString();
            DirectoryInfo categoryDirectory = new DirectoryInfo(Path.Join(_templateFolder.FullName, categoryName));
            FileInfo[] templateFileArray = categoryDirectory.GetFiles();

            if (templateFileArray.Length == 0)
            {
                throw new Exception("No templates found.");
            }
            ;

            int chosenIndex = 0;

            // See how many files are in array
            if (templateFileArray.Length > 1)
            {
                Random generator = new Random();
                chosenIndex = generator.Next(0, templateFileArray.Length);
            }

            string fileContent = File.ReadAllText(templateFileArray[chosenIndex].FullName);

            MadLibStoryTemplate template = JsonConvert.DeserializeObject<MadLibStoryTemplate>(fileContent);

            return template;
        }

        private static MadLibGenres GetMadlibGenre()
        {
            MadLibGenres selectedGenre = ConsoleUtility.SelectEnumOption(EnumUtility.GetValues<MadLibGenres>());
            return selectedGenre;
        }

        private static Dictionary<string, List<string>> GetExamplesForBlankType(List<MadLibBlank> blanksList)
        {
            Dictionary<string, List<string>> blankExamples = [];

            foreach (MadLibBlank blank in blanksList)
            {
                if (!blankExamples.ContainsKey(blank.Type))
                {
                    blankExamples[blank.Type] = GetExamplesFromFile(blank.Type);
                }
            }

            return blankExamples;
        }

        private static void InitializationChecks()
        {
            string categoryFolderPath = $"{_sourceDirectory}/Projects/Madlibs/MadlibBot/Templates/";
            bool doesFolderExist = Directory.Exists(categoryFolderPath);

            if (doesFolderExist)
            {
                // Pick random file in that folder
                string[] files = Directory.GetFiles(categoryFolderPath, "*.json");
                string randomFile = files[new Random().Next(files.Length)];
                JObject jsonText = JObject.Parse(File.ReadAllText(randomFile));

                // Deserialize
                MadLibStoryTemplate deserializedJson = JsonConvert.DeserializeObject<MadLibStoryTemplate>(jsonText.ToString());
                Console.WriteLine(deserializedJson);

            }
        }

        private static List<string> GetExamplesFromFile(string blankType)
        {
            string filePath = $"{_sourceDirectory}/Projects/Madlibs/MadlibBot/Words/{blankType}.json";
            bool doesFileExist = File.Exists(filePath);

            if (doesFileExist)
            {
                JArray fileContents = JArray.Parse(File.ReadAllText(filePath));
                List<string> wordList = fileContents.ToObject<List<string>>();
                return wordList.GetRange(0, 3);
            }

            return [];
        }

        private static void CreateStories(MadLibStoryTemplate storyTemplate, List<Player> gamePlayers)
        {
            foreach (Player currentPlayer in gamePlayers)
            {
                currentPlayer.MadlibStory = storyTemplate.Template;

                int blankIndex = 0;
                foreach (string player1Response in currentPlayer.MadLibResponse)
                {
                    MadLibBlank blank = storyTemplate.Blanks[blankIndex];
                    int templateCursorIndex = currentPlayer.MadlibStory.IndexOf($"{{{blank.Type}}}");

                    // Actually replace the blank in the story for the user
                    currentPlayer.MadlibStory = currentPlayer.MadlibStory.ReplaceFirst($"{{{blank.Type}}}", player1Response);

                    blankIndex++;
                    templateCursorIndex += blank.Type.Length + 2;
                }
            }

        }
        
        private static void VoteForBestStory(List<Player> gamePlayers)
        {
            ConsoleUtility.WriteColoredLine("we vote nao.", ConsoleColor.Yellow);

            foreach (Player currentPlayer in gamePlayers)
            {
                string voteForWinnerPrompt = $"{currentPlayer.Name}, OOGA! Tribe choose big booga caveman. Grunt number next ooga. Winner get mammoth.";
                PrintPlayerAndIndex(gamePlayers);
                int voteResponse = ConsoleUtility.GetIntInputFromUser(voteForWinnerPrompt, true);

                if ((voteResponse < 0) || voteResponse > gamePlayers.Count)
                {
                    bool isResponseValid = false;

                    while (!isResponseValid)
                    {
                        ConsoleUtility.WriteColoredLine("NO GOOD!!!!!", ConsoleColor.Red);

                        voteResponse = ConsoleUtility.GetIntInputFromUser(voteForWinnerPrompt, true);

                        isResponseValid = (voteResponse > 0) && voteResponse < gamePlayers.Count + 1;
                    }
                }

                Console.WriteLine($"player chose number: {voteResponse}");
                Console.WriteLine($"Which should be caveman: {gamePlayers[voteResponse - 1].Name}");
                
                gamePlayers[voteResponse - 1].Score++;
            }
        }

        private static void PrintPlayerAndIndex(List<Player> gamePlayers)
        {
            for (int i = 1; i <= gamePlayers.Count; i++)
            {
                Console.WriteLine($"{i}: {gamePlayers[i - 1].Name}");
            }
        }

        private static void DetermineWinner(List<Player> gamePlayers)
        {
            List<Player> sortedList = gamePlayers.OrderByDescending(p => p.Score).ToList();

            foreach(Player currentPlayer in sortedList)
            {
                ConsoleUtility.WriteColoredLine($"{currentPlayer.Name} has {currentPlayer.Score} points.", ConsoleColor.Yellow);
            }

            List<Player> playersWhoTiedForFirst = gamePlayers.Where(p => p.Score == sortedList[0].Score && sortedList[0].Name != p.Name).ToList();

            if (playersWhoTiedForFirst.Count > 0)
            {
                Console.WriteLine("Some cavemen tied. Fight to death");
                //PLay rock paper scissors
            }
        }
    }
}

