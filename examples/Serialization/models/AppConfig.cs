using Newtonsoft.Json;
using System.Collections.Generic;

namespace Serialization.Models
{
    public class AppConfig
    {
        [JsonProperty("appName")]
        public string AppName { get; set; }
        
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("hostURL")]
        public string HostURL { get; set; }

        [JsonProperty("userIds")]
        public List<int> UserIds { get; set; }

        [JsonProperty("adminUser")]
        public User AdminUser { get; set; }

        [JsonConstructor]
        public AppConfig() {}
    }
}