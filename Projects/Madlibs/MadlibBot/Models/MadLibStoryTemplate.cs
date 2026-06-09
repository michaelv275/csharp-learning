namespace MadLibBot.Models
{
    public class MadLibStoryTemplate
    {
        public string Title { get; set; }

        public string Template { get; set; }

        public List<MadLibBlank> Blanks { get; set; }

        public int BlankCount { get; set; }

        public string Category { get; set; }

    }
}