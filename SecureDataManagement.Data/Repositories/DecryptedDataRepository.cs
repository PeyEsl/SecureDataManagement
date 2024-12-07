using Microsoft.EntityFrameworkCore;
using SecureDataManagement.Core.Entities;
using SecureDataManagement.Core.Interfaces;

namespace SecureDataManagement.Data.Repositories
{
    public class DecryptedDataRepository : IDecryptedDataRepository
    {
        #region Ctor

        private readonly SecureDbContext _context;

        public DecryptedDataRepository(SecureDbContext context)
        {
            _context = context;
        }

        #endregion

        public async Task<bool> AddAsync(DecryptedData decryptedData)
        {
            try
            {
                await _context.Set<DecryptedData>()
                              .AddAsync(decryptedData);

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

                _context.Set<DecryptedData>()
                        .Remove(entity);

                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<DecryptedData>> GetAllAsync()
        {
            try
            {
                return await _context.Set<DecryptedData>()
                                     .ToListAsync();
            }
            catch
            {
                return Enumerable.Empty<DecryptedData>();
            }
        }

        public async Task<DecryptedData?> GetByEncryptedIdAsync(int encryptedId)
        {
            try
            {
                return await _context.Set<DecryptedData>()
                                     .FirstOrDefaultAsync(dd => dd.EncryptedDataId == encryptedId);
            }
            catch
            {
                return null;
            }
        }

        public async Task<DecryptedData?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<DecryptedData>()
                                     .FindAsync(id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(DecryptedData decryptedData)
        {
            try
            {
                _context.Set<DecryptedData>()
                        .Update(decryptedData);

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
