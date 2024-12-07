using AutoMapper;
using SecureDataManagement.Application.DTOs;
using SecureDataManagement.Core.Entities;
using SecureDataManagement.Web.Models.ViewModels;

namespace SecureDataManagement.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // موجودیت به Dto
            CreateMap<EncryptedData, EncryptedDataDto>();
            CreateMap<DecryptedData, DecryptedDataDto>();

            // Dto به موجودیت
            CreateMap<EncryptedDataDto, EncryptedData>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            CreateMap<DecryptedDataDto, DecryptedData>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            // Dto به ViewModel برای نمایش نتیجه
            CreateMap<EncryptedDataDto, EncryptedDataViewModel>().ReverseMap();
            CreateMap<DecryptedDataDto, DecryptedDataViewModel>().ReverseMap();
        }
    }
}
