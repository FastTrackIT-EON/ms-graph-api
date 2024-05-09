using Newtonsoft.Json;

namespace MsGraphApi.Models
{
    public abstract class GraphApiResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }
    }
}
