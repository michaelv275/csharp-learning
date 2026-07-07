using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using MadLibBot.Models;

namespace MadLibBot
{
    internal class Seeder
    {

        private static HttpClient _apiClient;

        static async Task SeedTemplates(string[] _1)
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
                    _ = Directory.CreateDirectory(categoryFolderPath);
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

                if (string.IsNullOrEmpty(response))
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
        }
    }
