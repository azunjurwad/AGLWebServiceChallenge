using Newtonsoft.Json;

namespace AGLWebServiceTest.Contracts
{
    /// <summary>
    /// Pet object contract
    /// </summary>
    public class Pet
    {
        [JsonProperty(PropertyName = "name")]
        public string PetName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string PetType { get; set; }
    }
}
