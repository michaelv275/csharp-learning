namespace AccessLevels.Models;

internal class Dog : Animal
{

    private struct _vetBill
    {
        public double Amount;
        public string Reason;
        
    }
    internal Dog(string humanGivenName)
    {
        Species = "Dog";
        Name = humanGivenName ?? "Ollie";
        Noise = "Woof";
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