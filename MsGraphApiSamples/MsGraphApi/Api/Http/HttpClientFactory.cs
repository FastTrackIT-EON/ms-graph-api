namespace MsGraphApi.Api.Http
{
    public static class HttpClientFactory
    {
        public static IGraphApiHttpClient CreateRealHttpClient(
            string baseUrl = "https://graph.microsoft.com",
            int timeoutSeconds = 30)
        {
            return new RealGraphApiHttpClient(baseUrl, timeoutSeconds);
        }

        public static IGraphApiHttpClient CreateFakeHttpClient(string getEmailMessagesJson)
        {
            return new FakeGraphApiHttpClient(getEmailMessagesJson);
        }
    }
}
