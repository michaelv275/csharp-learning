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
        CheckConfig();

        try
        {
            Console.WriteLine(_users[0].Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        // Returns first in the list or null if empty
        User userFake = _users.FirstOrDefault();
        Console.WriteLine(userFake?.Name ?? String.Empty);

        Dictionary<int, string> testDict = new Dictionary<int, string>()
        {
            {1, "test1"},
            {0, "test2"}
        };

        string val = testDict[1];

        // Check size
        // if (!_users.Any()) // bool if (_users.Count == 0) 
        // {
        //     List<User> dummyUsers = User.CreateDummies(4);
        //     // Adding to collection
        //     _users.AddRange(dummyUsers);
        // }

        // Print initial users
        // PrintUsers(_users);

        // _users[0].Email = "changeduremail@gmail.com";
        // PrintUsers(_users);

        // var evenUsers = _users.FindAll(user => user.Id % 2 == 0)
        // .Select(u => { return u.Id; });

        // foreach (int id in evenUsers)
        // {
        //     Console.WriteLine(id);
        // }

        // PrintUsers(evenUsers);

    }

    private static void CheckConfig()
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

    private static void PrintUsers(List<User> users)
    {
        users.ForEach((user) =>
        {
            Console.WriteLine($"User has name {user.Name} and email {user.Email}");
        });
    }
}


