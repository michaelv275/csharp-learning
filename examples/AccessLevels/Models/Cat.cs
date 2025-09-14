namespace AccessLevels.Models
{
    internal class Cat : Animal
    {
        internal Cat(string name)
        {
            Species = "Cat";
            Noise = "Meow";
            Name = name;
        }
    }
}