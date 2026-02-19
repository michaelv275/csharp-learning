using Newtonsoft.Json;

[Serializable]
public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("pronouns")]
    public List<string> Pronouns { get; set; }

    [JsonProperty("isAdmin")]
    public bool IsAdmin { get; set; }

    [JsonConstructor]
    public User() { }
    
    public static List<User> CreateDummies(int numDummies)
    {
        List<User> dummyUsers = new List<User>();
        for (int i = 0; i < numDummies; i++)
        {
            User user = new User()
            {
                Id = i + 1,
                Email = "dummy1@dispel.com",
                IsAdmin = true,
                Name = $"Dummy {i + 1}",
                Pronouns = new List<string>() { "she/him" }
            };

            dummyUsers.Add(user);
        }

        return dummyUsers;
    }
}