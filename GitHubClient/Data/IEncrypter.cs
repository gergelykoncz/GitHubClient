namespace GitHubClient.Data
{
    public interface IEncrypter
    {
        string DecryptString(string protectedBase64String);
        string EncryptString(string value);
    }
}
