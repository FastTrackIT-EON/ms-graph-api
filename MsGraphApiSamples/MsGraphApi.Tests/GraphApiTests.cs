using Microsoft.VisualStudio.TestTools.UnitTesting;
using MsGraphApi.Api;
using MsGraphApi.Api.Http;
using MsGraphApi.Models;
using MsGraphApi.Tests.Infrastructure;
using System.Threading.Tasks;

namespace MsGraphApi.Tests
{
    [TestClass]
    public class GraphApiTests
    {
        [TestMethod]
        public async Task GraphApi_WhenJsonWithMissingBodyPreview_DeserializesSuccessfully()
        {
            // arrange
            GraphApi graphApi = new GraphApi(
                HttpClientFactory.CreateFakeHttpClient(JsonSamples.JsonWithMissingBodyContentAndBodyPreview),
                new FakeLogger());

            // act
            graphApi.EstablishAuthenticationSession("the-access-token");
            GetEmailMessagesResponse result = await graphApi.GetMyEmails();

            // assert
            Assert.IsNotNull(result);
        }
    }
}
