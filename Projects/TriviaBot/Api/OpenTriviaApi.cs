using TriviaBot.Enums;

namespace TriviaBot.Api
{
    public class OpenTriviaApi
    {
        public static async Task GetQuestions(ApiHelper client, int amount = 10)
        {
            string jsonResponse = await client.CallApi();
            // Deserialize the JSON response into a list of questions
            var questions = JsonConvert.DeserializeObject<List<MultChoiceQuestion>>(jsonResponse);
        }

        private static string ParseErrorCode(OpenTriviaResponseCode errorCode)
        {
            string errorMessage = string.Empty;

            switch (errorCode)
            {
                case OpenTriviaResponseCode.NoResults:
                    errorMessage = "No Results: Could not return results. The API doesn't have enough questions for your query.";
                    break;
                case OpenTriviaResponseCode.InvalidParameter:
                    errorMessage = "Invalid Parameter: Contains an invalid parameter. Arguments passed in aren't valid.";
                    break;
                case OpenTriviaResponseCode.TokenNotFound:
                    errorMessage = "Token Not Found: Session Token does not exist.";
                    break;
                case OpenTriviaResponseCode.TokenEmpty:
                    errorMessage = "Token Empty: Session Token has returned all possible questions for the specified query. Resetting the Token is necessary.";
                    break;
                case OpenTriviaResponseCode.RateLimitExceeded:
                    errorMessage = "Rate Limit Exceeded: Too many requests have occurred. Each IP can only access the API once every 5 seconds.";
                    break;
                default:
                    errorMessage = "Unknown Error Code.";
                    break;
            }

            return errorMessage;
        }
    }
}