
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace MadlibBot.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MadLibGenres
    {
        Adventure,
        Fairytale,
        Funny,
        Romance,
    }
}
