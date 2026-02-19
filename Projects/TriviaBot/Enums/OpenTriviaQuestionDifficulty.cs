
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace TriviaBot.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OpenTriviaQuestionDifficulty
    {
        [EnumMember(Value = "easy")]
        Easy,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "hard")]
        Hard
    }
}
