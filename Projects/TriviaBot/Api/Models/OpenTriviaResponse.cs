using TriviaBot.Enums;

namespace TriviaBot.Api.Models
{
    public class OpenTriviaResponse
    {
        public OpenTriviaResponseCode ResponseCode { get; set; }
        public List<TriviaQuestion> Results { get; set; }
    }
}