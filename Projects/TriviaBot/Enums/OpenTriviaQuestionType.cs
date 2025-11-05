
using System.Runtime.Serialization;

[JsonConverter(typeof(StringEnumConverter))]
public enum OpenTriviaQuestionType
{
    [EnumMember(Value = "multiple")]
    MultiChoice,
    [EnumMember(Value = "boolean")]
    TrueFalse,
}