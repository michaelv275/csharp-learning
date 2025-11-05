
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

[JsonConverter(typeof(StringEnumConverter))]
public enum OpenTriviaQuestionType
{
    [EnumMember(Value = "multiple")]
    MultiChoice,
    [EnumMember(Value = "boolean")]
    TrueFalse,
}