using System.Threading.Tasks;

namespace MsGraphApi.Api.Http
{
    internal class FakeGraphApiHttpClient : IGraphApiHttpClient
    {
        private readonly string _getMailMessagesJson;

        public FakeGraphApiHttpClient(string getMailMessagesJson)
        {
            _getMailMessagesJson = getMailMessagesJson;
        }

        public void EnsureAuthentication(string accessToken)
        {
            /* don't care about the access token */
        }

        public async Task<string> GetMyEmailMessagesJson()
        {
            await Task.CompletedTask;

            return _getMailMessagesJson;
        }
    }
}
