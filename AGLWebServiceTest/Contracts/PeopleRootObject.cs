/*
 *Author: Abhishek Zunjurwad
 */

using Newtonsoft.Json;
using System.Collections.Generic;

namespace AGLWebServiceTest.Contracts
{
    /// <summary>
    /// Root object contract
    /// </summary>
    public class PeopleRootObject
    {
        [JsonProperty(PropertyName = "name")]
        public string PersonName { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "age")]
        public int Age { get; set; }

        [JsonProperty(PropertyName = "pets")]
        public IList<Pet> Pets { get; set; }
    }
}
