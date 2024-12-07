using SecureDataManagement.Core.Entities;

namespace SecureDataManagement.Core.Interfaces
{
    public interface IDecryptedDataRepository
    {
        Task<bool> AddAsync(DecryptedData decryptedData);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DecryptedData>> GetAllAsync();
        Task<DecryptedData?> GetByEncryptedIdAsync(int encryptedId);
        Task<DecryptedData?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(DecryptedData decryptedData);
    }
}
