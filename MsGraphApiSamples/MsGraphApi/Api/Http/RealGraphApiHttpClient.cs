using MsGraphApi.Exceptions;
using MsGraphApi.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsGraphApi.Api.Http
{
    internal class RealGraphApiHttpClient : IGraphApiHttpClient
    {
        private readonly HttpClient _httpClient;

        public RealGraphApiHttpClient(
            string baseUrl = "https://graph.microsoft.com",
            int timeoutSeconds = 30)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentNullException(
                    paramName: nameof(baseUrl),
                    message: "The base url cannot be null or empty.");
            }

            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out Uri baseUrlAsUri))
            {
                throw new ArgumentException(
                    paramName: nameof(baseUrl),
                    message: $"The base url '{baseUrl}' doesn't represent a valid absolute path.");
            }

            if (timeoutSeconds < 0)
            {
                throw new ArgumentException(
                    paramName: nameof(timeoutSeconds),
                    message: "Timeout must be a positive numeric value expressed in seconds.");
            }

            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _httpClient.BaseAddress = baseUrlAsUri;
        }

        public Uri BaseUrl { get; }

        public void EnsureAuthentication(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                accessToken);
        }

        public async Task<string> GetMyEmailMessagesJson()
        {
            // signal to server that we want a JSON response via Accept HTTP header
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // call the API
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(GraphApiEndpoints.GetMyEmails);
                if (response.IsSuccessStatusCode)
                {
                    if (response.Content != null)
                    {
                        string contentAsString = await response.Content.ReadAsStringAsync();
                        return contentAsString;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    throw new GraphApiException(
                        statusCode: (int)response.StatusCode,
                        reasonPhrase: response.ReasonPhrase,
                        // TODO: deserialize the error response
                        errorDetails: new Dictionary<string, object>());
                }
            }
            catch (TimeoutException timeoutEx)
            {
                throw new GraphApiException(
                    message: $"A timeout exception occured while calling GET {GraphApiEndpoints.GetMyEmails}",
                    innerException: timeoutEx,
                    statusCode: (int)HttpStatusCode.RequestTimeout,
                    reasonPhrase: HttpStatusCode.RequestTimeout.ToString(),
                    errorDetails: new Dictionary<string, object>());
            }
        }
    }
}
