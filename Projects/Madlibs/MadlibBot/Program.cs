using MadLibBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Madlibs.Utilities;
using MadlibBot.Enums;

namespace MadlibBot
{
    internal class Program
    {
        private static HttpClient _apiClient;
        private static DirectoryInfo _templateFolder;

        static void Main(string[] _1)
        {
            _templateFolder = new DirectoryInfo($"/Users/sophiapache/Documents/csharp-learning/Projects/Madlibs/MadlibBot/Templates/");

            List<Player> gamePlayers = GameIntroduction();
            foreach (Player player in gamePlayers)
            {
                Console.WriteLine($"Player: {player.Name}");
            }

            MadLibGenres madlibGenre = GetMadlibGenre();
            Console.WriteLine($"You selected {madlibGenre}");
            
            MadLibStoryTemplate storyTemplate = GetTemplate(madlibGenre);
            Console.WriteLine($"The {storyTemplate.Title} template has {storyTemplate.BlankCount} blanks");

            int testIndex = 0;
            foreach (MadLibBlank blank in storyTemplate.Blanks)
            {
                List<string> examples = GetExamplesForBlankType();
                foreach (Player currentCaveman in gamePlayers)
                {
                    ConsoleUtility.WriteColoredLine($"caveman {currentCaveman.Name} say {blank.Type} (ex. {string.Join(',', examples)})", ConsoleColor.Yellow);
                    currentCaveman.MadLibResponse.Add(ConsoleUtility.GetSecureUserInput());
                }

                testIndex++;

                if (testIndex >= 3)
                {
                    break;
                }
            }

            foreach (Player test in gamePlayers)
            {
                Console.WriteLine($"{test.Name} entered: {string.Join(',', test.MadLibResponse)}");
            }
            


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
            List<Player> playerList = new List<Player>();
            for (int i = 0; i < playerCount; i++)
            {
                string cavemanName = ConsoleUtility.GetUserInput("what your name? hit enter when done new players.");
                // Validate string input, make sure not null or empty. Write function
                playerList.Add(new Player(cavemanName));
            }

            playerList.Add(new Player("BoogaBot"));

            ConsoleUtility.WriteColoredLine("all players added. let play", ConsoleColor.Yellow);
            return playerList;
        }

        // Get a template object for a given category
        private static MadLibStoryTemplate? GetTemplate(MadLibGenres category)
        {
            string categoryName = category.ToString();
            DirectoryInfo categoryDirectory = new DirectoryInfo(Path.Join(_templateFolder.FullName, categoryName));
            FileInfo[] templateFileArray = categoryDirectory.GetFiles();

            if (templateFileArray.Length == 0) { 
                throw new Exception("No templates found.");
            };

            int chosenIndex = 0;

            // See how many files are in array
            if (templateFileArray.Length > 1) {
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

    private static List<string> GetExamplesForBlankType()
    {
        return ["ex1", "ex2"];
    }

    private static void InitializationChecks()
    {
        string categoryFolderPath = $"/Users/sophiapache/Documents/csharp-learning/Projects/Madlibs/MadlibBot/Templates/";
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
    
}
}

