namespace SecureDataManagement.Application.Interfaces
{
    public interface IDataProtectionService
    {
        string Protect(string plainText);
        string Unprotect(string protectedText);
    }
}
