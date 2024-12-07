using SecureDataManagement.Application.DTOs;

namespace SecureDataManagement.Application.Interfaces
{
    public interface IEncryptionService
    {
        Task<bool> AddAsync(EncryptedDataDto encryptedDataDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EncryptedDataDto>> GetAllAsync();
        Task<EncryptedDataDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(EncryptedDataDto encryptedDataDto);
    }
}
