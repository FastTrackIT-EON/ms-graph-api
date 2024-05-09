using Newtonsoft.Json;

namespace MsGraphApi.Models
{
    // https://learn.microsoft.com/en-us/graph/api/resources/itembody?view=graph-rest-1.0
    public class ItemBody
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }
    }
}
