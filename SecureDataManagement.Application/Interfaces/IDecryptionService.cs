using SecureDataManagement.Application.DTOs;

namespace SecureDataManagement.Application.Interfaces
{
    public interface IDecryptionService
    {
        Task<bool> AddAsync(DecryptedDataDto decryptedDataDto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<DecryptedDataDto>> GetAllAsync();
        Task<DecryptedDataDto?> GetByEncryptedIdAsync(int encryptedId);
        Task<DecryptedDataDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(DecryptedDataDto decryptedDataDto);
    }
}
