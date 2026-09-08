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
        private static string _sourceDirectory = @"C:\Dispel\Dispel-repos\csharp-learning";

        static void Main(string[] _1)
        {
            _templateFolder = new DirectoryInfo($"{_sourceDirectory}/Projects/Madlibs/MadlibBot/Templates/");

            List<Player> gamePlayers = GameIntroduction();

            foreach (Player player in gamePlayers)
            {
                Console.WriteLine($"Player: {player.Name}");
            }

            MadLibGenres madlibGenre = GetMadlibGenre();
            Console.WriteLine($"You selected {madlibGenre}");

            MadLibStoryTemplate storyTemplate = GetTemplate(madlibGenre);
            Console.WriteLine($"The {storyTemplate.Title} template has {storyTemplate.BlankCount} blanks");

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

            //Test output 1 story
            Player player1 = gamePlayers[0];
            player1.MadlibStory = storyTemplate.Template;
            Console.WriteLine($"Story: {storyTemplate.Title}: ");

            Console.WriteLine($"Player has {player1.MadLibResponse.Count} responses");

            int blankIndex = 0;
            foreach (string player1Response in player1.MadLibResponse)
            {
                MadLibBlank blank = storyTemplate.Blanks[blankIndex];
                int templateCursorIndex = player1.MadlibStory.IndexOf($"{{{blank.Type}}}");

                // Actually replace the blank in the story for the user
                player1.MadlibStory = player1.MadlibStory.ReplaceFirst($"{{{blank.Type}}}", player1Response);

                blankIndex++;
                templateCursorIndex += blank.Type.Length + 2;
            }

            Console.WriteLine($"Story = {player1.MadlibStory}");

            //Get user input for all blanks from template
            //Display the story with each users choices for "blanks"
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
            MadLibGenres selectedGenre = ConsoleUtility.GetUserSelection(EnumUtility.GetValues<MadLibGenres>());
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
    }
}

