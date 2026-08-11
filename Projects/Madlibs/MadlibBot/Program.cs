using MadLibBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Madlibs.Utilities;

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

            MadLibStoryTemplate adventureTemplate = GetTemplate("Adventure");

            Console.WriteLine($"The {adventureTemplate.Title} template has {adventureTemplate.BlankCount} blanks");

            //Prompt user for category
            //Get random template for category
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
        private static MadLibStoryTemplate? GetTemplate(string category)
        {
            DirectoryInfo categoryDirectory = new DirectoryInfo(Path.Join(_templateFolder.FullName, category));
            Console.WriteLine($"categoryDirectory = {categoryDirectory.FullName}");
            FileInfo[] templateFileArray = categoryDirectory.GetFiles();
            Console.WriteLine($"templateFileArray has {templateFileArray.Length} templates");

            if (templateFileArray.Length == 0) { 
                throw new Exception("No templates found.");
            };

            int chosenIndex = 0;

            // See how many files are in array
            if (templateFileArray.Length > 1) {
                Random generator = new Random();
                chosenIndex = generator.Next(0, templateFileArray.Length);
            } 

            Console.WriteLine($"chosenIndex = {chosenIndex}");
            Console.WriteLine($"Chosen file path = {templateFileArray[chosenIndex].FullName}");

            string fileContent = File.ReadAllText(templateFileArray[chosenIndex].FullName);
            JObject wholeFile = JObject.Parse(fileContent);
            JArray stories = (JArray) wholeFile["data"]["stories"];

            Console.WriteLine($"fileContent = {stories}");

            MadLibStoryTemplate template = stories.FirstOrDefault().ToObject<MadLibStoryTemplate>();
            //After cleaning up json template files:
            //MadLibStoryTemplate template = JsonConvert.DeserializeObject<MadLibStoryTemplate>(fileContent);
            
            Console.WriteLine($"template is null = {template is null}");

            return template;
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

