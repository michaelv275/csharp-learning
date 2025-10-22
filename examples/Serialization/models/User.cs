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
}