using Newtonsoft.Json;
namespace MadLibBot.Models
{
    [Serializable]
    public class MadLibBlank
    {
        // The index in the blanks array 
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}