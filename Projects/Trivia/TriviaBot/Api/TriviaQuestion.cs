using Newtonsoft.Json;
using TriviaBot.Enums;


namespace TriviaBot.Api
{
    [Serializable]
    public class TriviaQuestion
    {
        [JsonProperty("type")]
        public OpenTriviaQuestionType QuestionType { get; set; }

        [JsonProperty("difficulty")]
        public OpenTriviaQuestionDifficulty Difficulty { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }

        [JsonConstructor]
        public TriviaQuestion()
        {

        }
    }
}