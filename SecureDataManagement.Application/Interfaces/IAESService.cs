namespace SecureDataManagement.Application.Interfaces
{
    public interface IAESService
    {
        (string EncryptedText, string IV, string Key) Encrypt(string plainText);
        string Decrypt(string encryptedText, string iv, string key);
    }
}
