using System.Net.Http;

namespace TriviaBot.Api;

public class ApiHelper
{
    private HttpClient _caller { get; set; }

    public ApiHelper()
    {
        _caller = new HttpClient();
    }

    public async Task<string> CallApi()
    {
        string responseBody = string.Empty;

        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            using (HttpResponseMessage httpResponse = await _caller.GetAsync("https://opentdb.com/api.php?amount=10&category=28&difficulty=hard"))
            {
                httpResponse.EnsureSuccessStatusCode();

                responseBody = await httpResponse.Content.ReadAsStringAsync();
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }

        return responseBody;
    }
}
