using Newtonsoft.Json;
namespace MadLibBot.Models
{
    [Serializable]
    public class MadLibStoryTemplate
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("template")]
        public string Template { get; set; }
        [JsonProperty("blanks")]
        public List<MadLibBlank> Blanks { get; set; }

        [JsonProperty("blankCount")]
        public int BlankCount { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

    }
}