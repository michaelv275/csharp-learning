using Newtonsoft.Json;
using TriviaBot.Enums;

namespace TriviaBot.Api.Models
{
    public class OpenTriviaResponse
    {
        [JsonProperty("response_code")]
        public OpenTriviaResponseCode ResponseCode { get; set; }
        [JsonProperty("results")]
        public List<TriviaQuestion> Results { get; set; }
    }
}