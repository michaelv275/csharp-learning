using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serialization.Models;
namespace Serialization;

class Program
{
    private static string _fileName = "config.json";
    static void Main(string[] _)
    {
        string configFilepath = Path.Combine("/Users/dakota/csharp-learning/examples/Serialization", _fileName);
        FileInfo configFile = new FileInfo(configFilepath);

        try
        {
            if (configFile.Exists)
            {
                string appConfigFile = File.ReadAllText(configFile.FullName);

                JObject genericJson = JObject.Parse(appConfigFile);

                if (genericJson.ContainsKey("isAdmin") && genericJson["isAdmin"]!.Value<bool>() == true)
                {
                    Console.WriteLine("Welcome Admin!");
                }

                AppConfig config = JsonConvert.DeserializeObject<AppConfig>(appConfigFile);

                Console.WriteLine("Admin username is " + config.AdminUser.Name);
            } 
        }
        catch
        {
            Console.WriteLine("Something went wrong dawg");
        }
    }
}


