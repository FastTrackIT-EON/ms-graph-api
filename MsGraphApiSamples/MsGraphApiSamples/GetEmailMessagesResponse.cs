using Newtonsoft.Json;
using System.Collections.Generic;

namespace MsGraphApiSamples
{
    public class GetEmailMessagesResponse
    {
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        [JsonProperty("value")]
        public List<EmailMessage> EmailMessages { get; set; }
    }
}
