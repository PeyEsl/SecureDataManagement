namespace SecureDataManagement.Application.Interfaces
{
    public interface IKeyDerivationService
    {
        (string DerivedKey, string Salt) GenerateKey(string password);
        bool VerifyKey(string password, string storedKey, string storedSalt);
    }
}
