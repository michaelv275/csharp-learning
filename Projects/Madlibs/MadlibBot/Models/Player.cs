namespace MadLibBot.Models
{
    public class Player
    {
        public string Name { get; set; }

        public int Score { get; set; }

        public List<string> MadLibResponse { get; set; }

        public string MadlibStory { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            MadLibResponse = [];
            MadlibStory = string.Empty;
        }
    }
}