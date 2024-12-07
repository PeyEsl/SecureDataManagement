namespace SecureDataManagement.Application.Interfaces
{
    public interface IHashingService
    {
        string GenerateHash(string input);
        bool VerifyHash(string input, string hash);
    }
}
