using TriviaBot.Enums;

namespace TriviaBot.Api.Models
{
    public class OpenTriviaResponse
    {
        public OpenTriviaResponseCode ResponseCode { get; set; }
        public List<MultChoiceQuestion> Results { get; set; }
    }
}