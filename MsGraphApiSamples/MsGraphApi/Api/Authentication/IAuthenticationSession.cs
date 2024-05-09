namespace MsGraphApi.Http.Authentication
{
    public interface IAuthenticationSession
    {
        string AccessToken { get; }
    }
}
