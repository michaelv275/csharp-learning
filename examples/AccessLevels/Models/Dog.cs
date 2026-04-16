namespace AccessLevels.Models;

internal class Dog : Animal
{

    public string Breed { get; private set; }
    private struct _vetBill
    {
        public double Amount;
        public string Reason;
        
    }
    internal Dog(string humanGivenName, string breed)
    {
        Species = "Dog";
        Name = string.IsNullOrEmpty(humanGivenName) ? "Ollie" : humanGivenName;
        Noise = "Woof";
        Breed = string.IsNullOrEmpty(breed) ? "Mixed" : breed;
    }

    private void Woof()
    {
        object vetBill = new
        {
            Amount = 75.00,
            Reason = "Annual Checkup"
        };
    }
}