using MadLibBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MadlibBot
{
    internal class Program
    {
        private static HttpClient _apiClient;

        static async Task Main(string[] _1)
        {
           

            // What we need:
            // response will be a JSON string. Convert to JOBject
            // We need, obj.data.stories[0].title for deduping
            // obj.data.stories[0].template for the story template which has placeholders that match the label of the "blank" ex: Dr. {name}
            // obj.data.stories[0].blanks Which is an array of objects like:
            // {
            //     "id": 1,
            //     "type": "name",
            //     "label": "Name"
            // }
            // obs.data.stories[0].category
        }


        private static void GameIntroduction()
        {
            Console.WriteLine("welcome 2 madlibs games i caveman.");
            Console.WriteLine("enter words and i make story. you like? we play? yes? no? maybe? we play anyway.");
            Console.WriteLine("we start now.");

            string playerName = Console.ReadLine();
            while (!string.IsNullOrEmpty(playerName))
            {
                Console.WriteLine("what your name? hit enter when done new players.");
                playerName = Console.ReadLine();
                _ = new Player(playerName);
            }

            Console.WriteLine("all players added. let play");

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
