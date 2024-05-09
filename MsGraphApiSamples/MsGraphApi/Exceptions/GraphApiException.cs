using System;
using System.Collections.Generic;

namespace MsGraphApi.Exceptions
{
    public class GraphApiException : Exception
    {
        public GraphApiException(
            int statusCode,
            string reasonPhrase,
            Dictionary<string, object> errorDetails)
            : this("An error occured while calling Graph API", null, statusCode, reasonPhrase, errorDetails)
        {
        }

        public GraphApiException(
            string message,
            int statusCode,
            string reasonPhrase,
            Dictionary<string, object> errorDetails)
            : this(message, null, statusCode, reasonPhrase, errorDetails)
        {
        }

        public GraphApiException(
            string message,
            Exception innerException,
            int statusCode,
            string reasonPhrase,
            Dictionary<string, object> errorDetails)
            : base(message, innerException)
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
            this.ErrorDetails = errorDetails;
        }

        public int StatusCode
        {
            get;
        }

        public string ReasonPhrase { get; }

        public Dictionary<string, object> ErrorDetails { get; }
    }
}
