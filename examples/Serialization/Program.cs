using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serialization.Models;
namespace Serialization;

class Program
{
    private static string _fileName = "config.json";
    private static List<User> _users = new List<User>();

    private static List<String> _badExampleFileNames = new List<String>{"config.json", "badconfig.json", "emptyconfig.json", "randomconfig.json"};

    private static List<String> _exampleFileNames = new List<String>{"config.json"};

    static void Main(string[] _)
    {
        // Make function to loop through fileNames and checkconfig for each (test config cases)
        VerifyFileConfigs(_exampleFileNames);
        WriteAppConfig("newConfig.json");
    }

    private static void CheckConfig(string fileName)
    {
       // Verify file exists

       string pathToConfig = Path.Combine("/Users/sophiapache/Documents/csharp-learning/examples/Serialization", fileName);
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
       // -> Did not output Config Json gud. Had this error: Unhandled exception. Newtonsoft.Json.JsonReaderException: After parsing a value an unexpected character was encountered: ". Path 'adminUser.email', line 10, position 4....
       // What happens if empty? If file doesn't exist?
       // If empty -> deserializedJson.AppName = null, does not output config json gud
       // If file doesnt exist -> emptyfile.json Exists; False is outputted
       // Create a random json file not at all similar to AppConfig model
       // -> File exists and is true but it is also null



    }

    private static void VerifyFileConfigs(List<String> fileNames)
    {
        foreach (string file in fileNames)
        {
            CheckConfig(file);
        };
    }

    private static void WriteAppConfig(String newFileName)
    {
      AppConfig exampleAppConfig = new AppConfig()
      {
          HostURL = "/caveman",
          UserIds = [10, 11, 12],
          AdminUser = new User() {Id = 1, Name = "OogaBooga"},    
      };
      exampleAppConfig.AppName = "Caveman";
      exampleAppConfig.Language = "Caveman";

    // Serialize into JSON Object
      String serializeAppConfig = JsonConvert.SerializeObject(exampleAppConfig);

    // Convert to JObject

    JObject newJObjAppConfig = JObject.Parse(serializeAppConfig);

    // Write a file
    string pathToConfig = Path.Combine("/Users/sophiapache/Documents/csharp-learning/examples/Serialization", newFileName);
    File.WriteAllText(pathToConfig, newJObjAppConfig.ToString(Formatting.Indented));
    Console.WriteLine("Bery gud");

    }

    // private static void PrintUsers(List<User> users)
    // {
    // }
}


