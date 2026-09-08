using System.Text;

namespace Madlibs.Utilities
{
    public static class ConsoleUtility
    {
        public static void WriteColoredLine(string message, ConsoleColor color)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }

        public static void WriteColored(string message, ConsoleColor color)
        {
            ConsoleColor previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ForegroundColor = previousColor;
        }

        public static string GetSecureUserInput()
        {
            StringBuilder passwordBuilder = new StringBuilder();
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // Intercept the key press

                // Handle standard characters and the Backspace key
                if (char.IsControl(key.KeyChar))
                {
                    if (key.Key == ConsoleKey.Backspace && passwordBuilder.Length > 0)
                    {
                        _ = passwordBuilder.Remove(passwordBuilder.Length - 1, 1);
                        Console.Write("\b \b"); // Erase the character and the asterisk
                    }
                }
                else
                {
                    _ = passwordBuilder.Append(key.KeyChar);
                    Console.Write("*"); // Display an asterisk for the character
                }
            }
            while (key.Key != ConsoleKey.Enter); // Stop when the Enter key is pressed

            // Remove the newline character added by pressing Enter from the console output
            Console.WriteLine();
            return passwordBuilder.ToString();
        }

        public static int GetPositiveIntInputFromUser(string prompt)
        {
            int userValue = -1;
            //1. Display prompt
            WriteColored($"\n{prompt} ", ConsoleColor.Yellow);

            //2. Store initial user input (Console.ReadLine())
            string userInput = Console.ReadLine();

            //5. If valid, accept and return
            bool isInputValid = false;

            //3. loop to validate initial user input

            while (!isInputValid)
            {
                //Validate
                isInputValid = int.TryParse(userInput, out userValue);

                if (!isInputValid || userValue <= -1)
                {
                    // Recursively call to get valid input
                    return GetPositiveIntInputFromUser("bad bad!!! positive num ONLY >:(. Again!");
                }
            }

            return userValue;
        }

        private static int GetIntInputFromUser(string prompt)
        {
            WriteColored($"\n{prompt}", ConsoleColor.Yellow);
            bool isInputValid = int.TryParse(Console.ReadLine().Trim(), out int inputNumber);

            return isInputValid && inputNumber >= 0
                ? inputNumber
                : -1;
        }

        // string userNoun = GetUserInput("Enter a noun");

        public static string? GetUserInput(string prompt)
        {
            ArgumentException.ThrowIfNullOrEmpty(prompt);

            WriteColoredLine(prompt, ConsoleColor.Yellow);

            return Console.ReadLine();

        }

        public static T? GetUserSelection<T>(IEnumerable<T> options, string prompt = "Select 1 of the available options: ") where T : Enum
        {
            foreach (T option in options)
            {
                int underlyingValue = Convert.ToInt32(option);

                Console.WriteLine($"{underlyingValue}: {option}");
            }
            Console.WriteLine("99: Random");

            int underlyingGenreKey = GetIntInputFromUser(prompt);

            if (underlyingGenreKey < 0 || (underlyingGenreKey > options.Count() - 1 && underlyingGenreKey != 99))
            {
                WriteColoredLine("Invalid selection. Please try again.", ConsoleColor.Red);
                return GetUserSelection(options, prompt);
            }

            if (underlyingGenreKey == 99)
            {
                Random random = new Random();
                int randomIndex = random.Next(0, options.Count());
                return options.ElementAt(randomIndex);
            }

            T? selectedOption = options.ElementAtOrDefault(underlyingGenreKey);

            return selectedOption;
        }
    }
}
