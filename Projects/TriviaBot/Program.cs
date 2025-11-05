using TriviaBot.Api;
namespace TriviaBot;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        ApiHelper Api = new ApiHelper();

        string response = await Api.CallApi();

        Console.WriteLine($"Received response {response}");
    }
}
