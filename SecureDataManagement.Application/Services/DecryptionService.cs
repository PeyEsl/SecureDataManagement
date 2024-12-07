using AutoMapper;
using SecureDataManagement.Application.DTOs;
using SecureDataManagement.Application.Interfaces;
using SecureDataManagement.Core.Entities;
using SecureDataManagement.Core.Interfaces;

namespace SecureDataManagement.Application.Services
{
    public class DecryptionService : IDecryptionService
    {
        #region Ctor

        private readonly IDecryptedDataRepository _repository;
        private readonly IMapper _mapper;

        public DecryptionService(IDecryptedDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        public async Task<bool> AddAsync(DecryptedDataDto decryptedDataDto)
        {
            try
            {
                var entity = _mapper.Map<DecryptedData>(decryptedDataDto);

                return await _repository.AddAsync(entity);
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
                return await _repository.DeleteAsync(id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<DecryptedDataDto>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();

                return _mapper.Map<IEnumerable<DecryptedDataDto>>(entities);
            }
            catch (Exception)
            {
                return Enumerable.Empty<DecryptedDataDto>();
            }
        }

        public async Task<DecryptedDataDto?> GetByEncryptedIdAsync(int encryptedId)
        {
            try
            {
                var entity = await _repository.GetByEncryptedIdAsync(encryptedId);

                return _mapper.Map<DecryptedDataDto>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<DecryptedDataDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                return _mapper?.Map<DecryptedDataDto>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(DecryptedDataDto decryptedDataDto)
        {
            try
            {
                var entity = _mapper.Map<DecryptedData>(decryptedDataDto);

                return await _repository.UpdateAsync(entity);
            }
            catch
            {
                return false;
            }
        }
    }
}
