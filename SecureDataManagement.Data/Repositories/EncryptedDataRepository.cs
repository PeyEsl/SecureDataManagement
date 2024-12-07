using Microsoft.EntityFrameworkCore;
using SecureDataManagement.Core.Entities;
using SecureDataManagement.Core.Interfaces;

namespace SecureDataManagement.Data.Repositories
{
    public class EncryptedDataRepository : IEncryptedDataRepository
    {
        #region Ctor

        private readonly SecureDbContext _context;

        public EncryptedDataRepository(SecureDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> AddAsync(EncryptedData encryptedData)
        {
            try
            {
                await _context.Set<EncryptedData>()
                              .AddAsync(encryptedData);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null) return false;

                _context.Set<EncryptedData>()
                        .Remove(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<EncryptedData>> GetAllAsync()
        {
            try
            {
                return await _context.Set<EncryptedData>()
                                     .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<EncryptedData>();
            }
        }

        public async Task<EncryptedData?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<EncryptedData>()
                                     .FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(EncryptedData encryptedData)
        {
            try
            {
                _context.Set<EncryptedData>()
                        .Update(encryptedData);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
