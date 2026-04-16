namespace AccessLevels.Models
{
    public class Animal
    {
        private int _age;
        private DateTime? _birthdate;

        internal string Noise { get; set; }

        public string Species { get; set; }
        public string Name { get; set; }
        public int Age
        {
            // => is shorthand for "return" typically used in getters of properties.
            get => _age;
            private set => _age = value;
        }
        public DateTime? Birthdate
            //Both of these are acceptable ways to write a property that has a getter and private setter
            => _birthdate;

        public Animal()
        {
            // Default constructor
        }
        public string GetBirthday()
        {
            DateTime animalBirthday = _birthdate ?? DateTime.Now.AddYears(-Age);

            return animalBirthday.ToString("yyyy-MM-dd");
        }
        public static Animal MakeCat(string humanGivenName, int animalAge, DateTime? birthdate = null)
        {
            Cat createdCat = new Cat(humanGivenName)
            {
                Age = animalAge,             //If no birthdate given, create a random one based on the age given and name for fun.
                _birthdate = birthdate ?? DateTime.Now.AddYears(-animalAge).AddDays(-(humanGivenName.Length * 3))
            };

            return createdCat;
        }

        // create MakeDog function
        // Species, noise, etc

        public static Animal MakeDog(string humanGivenName, int animalAge, string Noise = "woof", string Breed = "Mixed", DateTime? birthdate = null)
        {
            Dog createdDog = new Dog(humanGivenName, Breed)
            {
                Age = animalAge,
                Noise = Noise,
                _birthdate = birthdate ?? DateTime.Now.AddYears(-animalAge).AddDays(-(humanGivenName.Length * 2))
            };


            return createdDog;
        }

        public virtual void Speak()
        {
            Console.WriteLine($"{Name} the {Species} says {Noise}");
        }

    }
}