namespace SecureDataManagement.Application.Interfaces
{
    public interface IHMACService
    {
        void SetKey(string base64Key);
        string GenerateSignature(string data);
        bool VerifySignature(string data, string signature);
    }
}
