using MsGraphApi.Api.Http;
using MsGraphApi.Exceptions;
using MsGraphApi.Http.Authentication;
using MsGraphApi.Infrastructure;
using MsGraphApi.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MsGraphApi.Api
{
    public class GraphApi
    {
        private readonly IGraphApiHttpClient _httpClient;
        private readonly ILogger _logger;
        private string _accessToken;

        public GraphApi(
            IGraphApiHttpClient httpClient,
            ILogger logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void EstablishAuthenticationSession(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new ArgumentException(
                    paramName: nameof(accessToken),
                    message: "Authentication session access token must be not null, not empty and not whitespaces only.");
            }
            _accessToken = accessToken;
        }

        public async Task<GetEmailMessagesResponse> GetMyEmails()
        {
            _httpClient.EnsureAuthentication(_accessToken);

            try
            {
                string json = await _httpClient.GetMyEmailMessagesJson();

                GetEmailMessagesResponse response = JsonConvert.DeserializeObject<GetEmailMessagesResponse>(
                    json);

                return response;
            }
            catch (GraphApiException ex)
            {
                _logger.LogException(ex);

                return null;
            }
        }
    }
}
