using SecureDataManagement.Core.Entities;

namespace SecureDataManagement.Core.Interfaces
{
    public interface IEncryptedDataRepository
    {
        Task<bool> AddAsync(EncryptedData encryptedData);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EncryptedData>> GetAllAsync();
        Task<EncryptedData?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(EncryptedData encryptedData);
    }
}
