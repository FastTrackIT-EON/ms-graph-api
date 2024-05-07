using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace MsGraphApiSamples
{
    // https://learn.microsoft.com/en-us/graph/api/resources/eventmessage?view=graph-rest-1.0
    public class EmailMessage
    {
        [JsonProperty("bccRecipients")]
        public List<EmailRecipient> BccRecipients { get; set; } = new List<EmailRecipient>();

        [JsonProperty("body")]
        public ItemBody Body { get; set; } = new ItemBody();

        [JsonProperty("bodyPreview")]
        public string BodyPreview { get; set; }

        [JsonProperty("categories")]
        public List<string> Categories { get; set; }

        [JsonProperty("ccRecipients")]
        public List<EmailRecipient> CcRecipients { get; set; } = new List<EmailRecipient>();

        [JsonProperty("changeKey")]
        public string ChangeKey { get; set; }

        [JsonProperty("conversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("conversationIndex")]
        public string ConversationIndex { get; set; }

        [JsonConverter(typeof(IsoDateTimeConverter))]
        [JsonProperty("createdDateTime")]
        public DateTimeOffset CreatedDateTime { get; set; }

        // TODO: flag

        [JsonProperty("from")]
        public EmailRecipient From { get; set; } = new EmailRecipient();

        // TODO: hasAttachments

        [JsonProperty("id")]
        public string Id { get; set; }

        // TODO: ...

        [JsonProperty("isRead")]
        public bool IsRead { get; set; }

        // TODO: ...

        [JsonProperty("sender")]
        public EmailRecipient Sender { get; set; } = new EmailRecipient();

        // TODO: ...

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("toRecipients")]
        public List<EmailRecipient> ToRecipients { get; set; } = new List<EmailRecipient>();
    }
}
