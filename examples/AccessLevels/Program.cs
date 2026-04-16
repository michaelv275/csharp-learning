using System.Data.Common;
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

            // Console.WriteLine("Enter some string");
            // string userNumbersAsString = Console.ReadLine();
            
            // Console.WriteLine($"You entered (as string): {userNumbersAsString}");

            // string orginalPlus2 = userNumbersAsString + 2;

            // Console.WriteLine($"original (string) + 2 = {orginalPlus2}");

            // Console.WriteLine($"Dog's name is {dog1.Name}. Would you like to change it? (y/n) (yes/no)");
            // string[] yesAnswers = new string[] { "y", "yes" };
            // string[] noAnswers = new string[] { "n", "no" };

            // string userResponse = Console.ReadLine();
            // if (yesAnswers.Contains(userResponse.ToLower()))
            // {
            //     Console.WriteLine($"What would you like to rename {dog1.Name} to?");
            //     string newName = Console.ReadLine();
            //     dog1.Name = String.IsNullOrEmpty(newName) ? dog1.Name : newName;
            //     Console.WriteLine($"The new dog name is {dog1.Name}");
            // } else
            // {
            //     Console.WriteLine($"The dog's name remains {dog1.Name}");
            // }

            // Console.WriteLine($"Congratulations, your dog's name is {dog1.Name}");

            // int maxTries = 3;
            // int currentTry = 0;
            // bool isUserInputValid = false;
            // while (!isUserInputValid && currentTry <= maxTries)
            // {
            //     isUserInputValid = CheckUserInput();
            //     Console.WriteLine($"Counter is at {currentTry}");
            //     currentTry++;
            // }
            
            // HOMEWORK:
            // turn off autocomplete in this repo
            /* Ask a user if they want to rename a dog:
            * If yes, let them rename
            * If no, Keep the same name
            * if unsure, ask them again until they give a valid answer
            * At the end of the program ask them if they would like to see the other dog, and repeat.
            */  

            public static void ChangeDogName(string[] dogList)
        {
            // for each Dog in dogList
            foreach (var dog in dogList)
            {
                Console.WriteLine($"Dog's name is {dog.Name}. Would you like to change it? (y/n) (yes/no)");
                string[] yesAnswers = new string[] { "y", "yes" };
                string[] noAnswers = new string[] { "n", "no" };

                string userResponse = Console.ReadLine();

                int maxTries = 3;
                int currentTry = 0;
                bool isUserInputValid = false;
                while (!isUserInputValid && currentTry <= maxTries)
                {
                    isUserInputValid = CheckUserInput();
                    Console.WriteLine($"Counter is at {currentTry}");
                    currentTry++;
                }

                if (yesAnswers.Contains(userResponse.ToLower()))
                {
                    Console.WriteLine($"What would you like to rename {dog.Name} to?");
                    string newName = Console.ReadLine();
                    dog.Name = String.IsNullOrEmpty(newName) ? dog.Name : newName;
                    Console.WriteLine($"The new dog name is {dog.Name}");
                } else
                {
                    Console.WriteLine($"The dog's name remains {dog.Name}");
                }

                Console.WriteLine($"Congratulations, your dog's name is {dog.Name}. Would you like to name another one? (y/n)");
                string userResponse2 = Console.ReadLine();
                if (!yesAnswers.Contains(userResponse2.ToLower())) break;
            }

        }



        }
    }
}
