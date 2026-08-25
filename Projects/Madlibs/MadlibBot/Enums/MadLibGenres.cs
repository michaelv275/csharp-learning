
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MadlibBot.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MadLibGenres
    {
        [EnumMember(Value = "Adventure")]
        Adventure,
        [EnumMember(Value = "fairytale")]
        Fairytale,
        [EnumMember(Value = "funny")]
        Funny,
        [EnumMember(Value = "romance")]
        Romance,
    }
}
