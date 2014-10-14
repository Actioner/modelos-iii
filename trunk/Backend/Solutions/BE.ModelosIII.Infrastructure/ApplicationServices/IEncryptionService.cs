namespace BE.ModelosIII.Infrastructure.ApplicationServices
{
    public interface IEncryptionService
    {
        string EncryptPassword(string plainPassword);

        string EncryptToken(string message);
        string DecryptToken(string message);
    }
}