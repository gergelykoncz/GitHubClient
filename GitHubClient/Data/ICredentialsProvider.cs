namespace GitHubClient.Data
{
    public interface ICredentialsProvider
    {
        void EraseCredentials();
        string GetPassword();
        string GetUserName();
        void StoreCredentials(string userName, string password);
    }
}
