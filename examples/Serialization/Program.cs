using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serialization.Models;
namespace Serialization;

class Program
{
    private static string _fileName = "config.json";
    private static List<User> _users = new List<User>();
    static void Main(string[] _)
    {
        // Make function to loop through fileNames and checkconfig for each (test config cases)
        CheckConfig();

    }

    private static void CheckConfig()
    {
       // Verify file exists

       string pathToConfig = Path.Combine("/Users/sophiapache/Documents/csharp-learning/examples/Serialization", _fileName);
        FileInfo configFile = new FileInfo(pathToConfig);
        Console.WriteLine($"{configFile.Name} Exists; {configFile.Exists}");
        if (configFile.Exists)
        {
            // Parsing
            JObject configJson = JObject.Parse(File.ReadAllText(pathToConfig));

            // Deserialize into AppConfig
            AppConfig deserializedJson = JsonConvert.DeserializeObject<AppConfig>(configJson.ToString());
            if (deserializedJson.AppName != null)
            {
                Console.WriteLine("Config JSON gud");
            }
        }

        
       // Handle Errors HOMEWORK
       // What would happen if JSON was malformed? Create bad config.json files
       // What happens if empty? If file doesn't exist?
       // Create a random json file not at all similar to AppConfig model



    }

    private static void PrintUsers(List<User> users)
    {
    }
}


