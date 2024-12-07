namespace SecureDataManagement.Application.Interfaces
{
    public interface IRSAService
    {
        (string PublicKey, string PrivateKey) GenerateKeys();
        string Encrypt(string plainText, string publicKey);
        string Decrypt(string encryptedText, string privateKey);
    }
}
