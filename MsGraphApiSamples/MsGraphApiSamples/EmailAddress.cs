using Newtonsoft.Json;

namespace MsGraphApiSamples
{
    // https://learn.microsoft.com/en-us/graph/api/resources/emailaddress?view=graph-rest-1.0
    public class EmailAddress
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
