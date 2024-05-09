using System.Threading.Tasks;

namespace MsGraphApi.Api.Http
{
    public interface IGraphApiHttpClient
    {
        void EnsureAuthentication(string accessToken);

        Task<string> GetMyEmailMessagesJson();
    }
}
