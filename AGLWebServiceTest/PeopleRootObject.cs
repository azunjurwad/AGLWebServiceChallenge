using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGLWebServiceTest
{
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
