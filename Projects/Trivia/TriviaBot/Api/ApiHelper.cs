namespace TriviaBot.Api;

public class ApiHelper
{
    private HttpClient _caller { get; set; }

    public ApiHelper(string hostAddress)
    {
        _caller = new HttpClient
        {
            BaseAddress = new Uri(hostAddress)
        };
    }

    public async Task<string> CallApi(string urlParameters)
    {
        string responseBody = string.Empty;

        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            Uri apiRequestUri = new Uri(_caller.BaseAddress + urlParameters);
            using (HttpResponseMessage httpResponse = await _caller.GetAsync(apiRequestUri))
            {
                _ = httpResponse.EnsureSuccessStatusCode();

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
