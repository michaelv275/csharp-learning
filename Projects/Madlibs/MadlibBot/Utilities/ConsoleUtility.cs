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
            ConsoleUtility.WriteColored($"\n{prompt} ", ConsoleColor.Yellow);
            
            //2. Store initial user input (Console.ReadLine())
            string userInput = Console.ReadLine();


            //5. If valid, accept and return
            bool isInputValid = false;

            //3. loop to validate initial user input

            while (!isInputValid)
            {
                //Validate
                bool isInteger = int.TryParse(userInput, out userValue);
            }

            //4. If invalid, reprompt

            if (userValue <= -1)
            {
                // Recursively call to get valid input
                userValue = GetPositiveIntInputFromUser("bad bad!!! positive num ONLY >:(");
            }

            return userValue;
        }
    }
    
    
}
