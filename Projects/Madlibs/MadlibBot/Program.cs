using MadLibBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Madlibs.Utilities;

namespace MadlibBot
{
    internal class Program
    {
        private static HttpClient _apiClient;

        static void Main(string[] _1)
        {
           List<Player> gamePlayers = GameIntroduction();
        }


        private static List<Player> GameIntroduction()
        {
            Console.WriteLine("welcome 2 madlibs games i caveman.");
            Console.WriteLine("enter words and i make story. you like? we play? yes? no? maybe? we play anyway.");
            Console.WriteLine("we start now.");

            // Ask how many human players are playing
            int playerCount = ConsoleUtility.GetPositiveIntInputFromUser("how many cavemen ooga booga");
            return new List<Player>();

            // Collect user input as int
            // Loop through and collect names
            // Add caveman at the end.

            string playerName = Console.ReadLine();
            while (!string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("what your name? hit enter when done new players.");
                playerName = Console.ReadLine();
                _ = new Player(playerName);
            }

            Console.WriteLine("all players added. let play");
            //return playerListVar
        }

        // Get template and blanks from response
        private static void GetTemplate()
        {
            // Pick a random template from the Templates folder
            List<string> randomCategory = ["Adventure", "Mystery", "Fantasy"];
            string category = randomCategory[new Random().Next(randomCategory.Count)];
            string categoryFolderPath = $"/Users/sophiapache/Documents/csharp-learning/Projects/Madlibs/MadlibBot/Templates/{category}/";
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

            // Extract the blanks obj from the templates json file

            // Loop through blanks and prompt user for input

        }
    }
}

// Homework:
// create model for response object to deserialize into instead of using JObject (MadLibResponse)
// Subobjects: Blanks
