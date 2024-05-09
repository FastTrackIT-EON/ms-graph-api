using Newtonsoft.Json;
using System.Collections.Generic;

namespace MsGraphApi.Models
{
    public abstract class GraphApiListResponse<TElement> : GraphApiResponse
    {
        [JsonProperty("value")]
        public List<TElement> Values { get; set; }
    }
}
