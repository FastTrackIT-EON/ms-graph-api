using Newtonsoft.Json;

namespace MsGraphApiSamples
{
    // https://learn.microsoft.com/en-us/graph/api/resources/recipient?view=graph-rest-1.0
    public class EmailRecipient
    {
        [JsonProperty("emailAddress")]
        public EmailAddress EmailAddress { get; set; } = new EmailAddress();
    }
}
