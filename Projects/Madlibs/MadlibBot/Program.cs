using System.Net.Http.Headers;
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
            string headerKey = "X-API-Key";
            string sophiaApiToken = "";
            _apiClient = new HttpClient();

            SetRequestHeaders(headerKey, sophiaApiToken);

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    string jsonString = await GetApiResponse();

                    

                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        WriteFiles(jsonString);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"*Error* Could not make request. Error: {ex.Message}");
                }
            }

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

        private static void SetRequestHeaders(string headerKey, string headerValue)
        {
            HttpRequestHeaders requestHeaders = _apiClient.DefaultRequestHeaders;
            requestHeaders.Add(headerKey, headerValue);
        }

        private static void WriteFiles(string rawJsonResponse)
        {
            JObject rawObj = JObject.Parse(rawJsonResponse);

            if (rawObj["data"]["stories"] != null)
            {
                // Assumes Story is a custom class you already created to match the data shape you want
                List<MadLibStoryTemplate> responseObj = JObject.Parse(rawJsonResponse)["data"]["stories"].ToObject<List<MadLibStoryTemplate>>();

                // check category of response and write to file based on category (if exists, write to existing file, if not, create new file
                string category = responseObj[0].Category;
                string categoryFolderPath =
                    $"/Users/sophiapache/Documents/csharp-learning/Projects/Madlibs/MadlibBot/Templates/{category}/";
                bool doesFolderExist = Directory.Exists(categoryFolderPath);

                if (!doesFolderExist)
                {
                    Directory.CreateDirectory(categoryFolderPath);
                }

                // check title of response and check if title already exists in file for that category. If it does, skip, if not, write to file)
                string escapedtitle = responseObj[0].Title.Replace(' ', '-');
                string titleFilePath = $"{categoryFolderPath}{escapedtitle}.json";
                bool doesFileExist = File.Exists(titleFilePath);

                if (!doesFileExist)
                {
                    File.WriteAllText(titleFilePath, rawJsonResponse);
                }
                else
                {
                    Console.WriteLine(
                        $"File with title {escapedtitle} already exists for category {category}"
                    );
                }
            }


        }

        private static async Task<string> GetApiResponse()
        {
            string baseURL = "https://api.apiverve.com/v1/madlibs";
            string response = "";

            Uri requestEndpoint = new Uri(baseURL);
            HttpResponseMessage httpResponse;

            httpResponse = await _apiClient.GetAsync(requestEndpoint);

            if (httpResponse.Content != null)
            {
                response = await httpResponse.Content.ReadAsStringAsync();

                if (String.IsNullOrEmpty(response))
                {
                    Console.WriteLine("Response was null or empty");
                    return "";
                }
            }
            else
            {
                Console.WriteLine("Content was null");
            }

            return response;
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
                new Player.Player(playerName);
            }

            Console.WriteLine("all players added. let play");

        }
    }
}

// Homework:
// create model for response object to deserialize into instead of using JObject (MadLibResponse)
// Subobjects: Blanks
