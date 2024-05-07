using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MsGraphApiSamples
{
    public static class GraphApi
    {
        private static string _accessToken;
        private static Uri _baseAddress = new Uri("https://graph.microsoft.com");

        public static string AccessToken
        {
            get { return _accessToken; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(
                        paramName: nameof(value),
                        message: "Access token cannot be null, or empty, or whitespaces-only.");
                }

                _accessToken = value;
            }
        }

        public static Uri BaseAddress
        {
            get { return _baseAddress; }
            set
            { 
                if (value is null)
                {
                    throw new ArgumentNullException(
                        paramName: nameof(value),
                        message: "The Graph API base address cannot be null.");
                }

                _baseAddress = value;
            }
        }

        public static async Task<string> GetJsonAsync(string endpointRelativeUrl)
        {
            if (string.IsNullOrWhiteSpace(endpointRelativeUrl))
            {
                throw new ArgumentNullException(
                    paramName: nameof(endpointRelativeUrl),
                    message: "The endpoint relative url must not be null, or empty, or whitespaces only.");
            }

            if (!Uri.TryCreate(endpointRelativeUrl, UriKind.Relative, out Uri uri))
            {
                throw new ArgumentException(
                    paramName: nameof(endpointRelativeUrl),
                    message: $"The endpoint relative url '{endpointRelativeUrl}' is not a valid relative Uri.");
            }

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    AccessToken);

                HttpResponseMessage response = await client.GetAsync(endpointRelativeUrl);
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
                        (int)response.StatusCode,
                        response.ReasonPhrase,
                        new Dictionary<string, object>());
                }
            }
        }
    }
}
