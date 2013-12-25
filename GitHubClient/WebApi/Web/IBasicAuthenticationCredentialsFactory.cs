namespace GitHubClient.WebApi.Web
{
    public interface IBasicAuthenticationCredentialsFactory
    {
        string CreateCredentials();
        string CreateCredentials(string userName, string password);
    }
}
