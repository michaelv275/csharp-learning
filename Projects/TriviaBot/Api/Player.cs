using Newtonsoft.Json;

namespace TriviaBot.Api
{
    public class Player
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int NumIncorrectAnswers { get; set; }

        public Player(string name)
        {
            Name = name;
            Score = 0;
            NumIncorrectAnswers = 0;
        }
    }
}