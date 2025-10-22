using System.Collections.Generic;
using AccessLevels.Models;
using System;
namespace AccessLevels
{
    class Program
    {
        private static List<Animal> _animals = new List<Animal>();

        static void Main(string[] args)
        {
            _ = args;
            Console.WriteLine("Starting up some animals...\r\n");

            _animals.Add(Animal.MakeCat("Whiskers", 3));
            _animals.Add(Animal.MakeCat("Whiskers_2", 2));
            _animals.Add(Animal.MakeCat("Mittens", 6, new DateTime(2019, 3, 15)));
            _animals.Add(Animal.MakeDog("Ollie", 5, new DateTime(2019, 10, 21)));

            Console.WriteLine("What do the animals have to say?");
            foreach (var animal in _animals)
            {
                animal.Speak();
            }

            Console.WriteLine();
            Console.WriteLine("The animal's birthdays are:");
            foreach (Animal critter in _animals)
            {
                Console.WriteLine($"{critter.Name}'s birthday is {critter.GetBirthday()}");
            }

            Console.WriteLine("\r\nAll done!");
        }
    }
}
