using AutoMapper;
using SecureDataManagement.Application.DTOs;
using SecureDataManagement.Application.Interfaces;
using SecureDataManagement.Core.Entities;
using SecureDataManagement.Core.Interfaces;

namespace SecureDataManagement.Application.Services
{
    public class EncryptionService : IEncryptionService
    {
        #region Ctor

        private readonly IEncryptedDataRepository _repository;
        private readonly IMapper _mapper;

        public EncryptionService(IEncryptedDataRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #endregion

        public async Task<bool> AddAsync(EncryptedDataDto encryptedDataDto)
        {
            try
            {
                var entity = _mapper.Map<EncryptedData>(encryptedDataDto);

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

        public async Task<IEnumerable<EncryptedDataDto>> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();

                return _mapper.Map<IEnumerable<EncryptedDataDto>>(entities);
            }
            catch
            {
                return Enumerable.Empty<EncryptedDataDto>();
            }
        }

        public async Task<EncryptedDataDto?> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                return _mapper?.Map<EncryptedDataDto>(entity);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(EncryptedDataDto encryptedDataDto)
        {
            try
            {
                var entity = _mapper.Map<EncryptedData>(encryptedDataDto);

                return await _repository.UpdateAsync(entity);
            }
            catch
            {
                return false;
            }
        }
    }
}
