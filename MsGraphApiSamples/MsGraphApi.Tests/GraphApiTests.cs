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
            string sampleJson = JsonSamples.ReadResourceJson("ResponseWithMissingBodyContentAndBodyPreview");

            GraphApi graphApi = new GraphApi(
                HttpClientFactory.CreateFakeHttpClient(sampleJson),
                new FakeLogger());

            // act
            graphApi.EstablishAuthenticationSession("the-access-token");
            GetEmailMessagesResponse result = await graphApi.GetMyEmails();

            // assert
            Assert.IsNotNull(result);
        }
    }
}
