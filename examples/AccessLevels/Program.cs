using AccessLevels.Models;
namespace AccessLevels
{
    class Program
    {
        private static List<Animal> _animals = [];

        static void Main(string[] args)
        {
            _ = args;

            Cat cat1 = Animal.MakeCat("Whiskers", 5) as Cat;
            Animal cat2 = Animal.MakeCat("Bob", 2);
            Animal dog1 = Animal.MakeDog("Zeppelin", 3, "Bork", "Pitbull");
            Animal dog2 = Animal.MakeDog("Kirby", 8, "Grr", "Poodle");

            _animals.Add(cat1);
            _animals.Add(cat2);
            _animals.Add(dog1);
            _animals.Add(dog2);

            // List<Dog> dogList = _animals.Where(A => A.GetType() == typeof(Dog)).ToList<Dog>();

            ChangeDogName(_animals);
        }

        /*
            Multi line comment
            
            example
        */
        public static void ChangeDogName(List<Animal> dogList)
        {
            Dictionary<string, string> renamedAnimals = [];
            foreach (Animal dog in dogList)
            {
                string[] yesAnswers = new string[] { "y", "yes" };
                string[] noAnswers = new string[] { "n", "no" };
                string[] approvedAnswers = yesAnswers.Concat(noAnswers).ToArray();

                int maxTries = 3;
                int currentTry = 0;
                bool isUserInputValid = false;

                Console.WriteLine($"Dog's name is {dog.Name}. Would you like to change it? (y/n) (yes/no)");
                string userResponse = Console.ReadLine();

                while (!isUserInputValid && currentTry <= maxTries)
                {
                    isUserInputValid = CheckUserInput(userResponse, allowableResponses: approvedAnswers);
                    if (!isUserInputValid)
                    {
                        Console.WriteLine($"You answered: \"{userResponse}\". The allowable responses are: \"{string.Join(',', approvedAnswers)}\". You are on {currentTry} out of {maxTries}.");
                        userResponse = Console.ReadLine();
                    }
                    currentTry++;
                }
                if (yesAnswers.Contains(userResponse.ToLower()))
                {
                    Console.WriteLine($"What would you like to rename {dog.Name} to?");
                    string newName = Console.ReadLine();
                    renamedAnimals[dog.Name] = String.IsNullOrEmpty(newName) ? dog.Name : newName;
                    dog.Name = String.IsNullOrEmpty(newName) ? dog.Name : newName;
                    Console.WriteLine($"The new dog name is {dog.Name}");
                } else
                {
                    Console.WriteLine($"The dog's name remains {dog.Name}");
                }

                Console.WriteLine($"Congratulations, your dog's name is {dog.Name}. Would you like to name another one? (y/n)");
                string userResponse2 = Console.ReadLine();
                if (!yesAnswers.Contains(userResponse2.ToLower()))
                {
                    if (renamedAnimals.Count > 0)
                    {
                        foreach(string name in renamedAnimals.Keys)
                        {
                            Console.WriteLine($"Original Name: {name}, New Name: {renamedAnimals[name]}");
                        }

                    }

                    break;
                };
            }
        }
        
        /// <summary>
        /// Validates whether the user's input matches any of the acceptable response options.
        /// </summary>
        /// <param name="userActualResponse">The actual response provided by the user.</param>
        /// <param name="yesResponses">Array of acceptable affirmative responses (e.g., "y", "yes").</param>
        /// <param name="denialResponses">Array of acceptable negative responses (e.g., "n", "no").</param>
        /// <returns>
        /// <c>true</c> if the user's response (case-insensitive) matches any value in either 
        /// the yesResponses or denialResponses arrays; otherwise, <c>false</c>.
        /// </returns>
        private static bool CheckUserInput(string userActualResponse, string[]? yesResponses = null, string[]? denialResponses = null, string[]? allowableResponses = null)
        {
            // Set initial return value
            bool isUserInputAcceptable = false;

            // Check if input is valid or not.
            // combine arrays, use method contains to check

            // allowableResponses = yesResponses.Concat(denialResponses).ToArray();
            isUserInputAcceptable = allowableResponses.Contains(userActualResponse);


            // Change isUserInputAcceptable to true if valid

            return isUserInputAcceptable;
        }
    }
}
