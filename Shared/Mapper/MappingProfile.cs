using AutoMapper;
using Shared.AuthorDtos;
using Shared.Models;
using Shared.TitleDtos;

namespace Shared.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Title mappings - DTO'daki küçük harf özelliklerini Entity'deki PascalCase özelliklere eşler
            CreateMap<UpdateTitleDto, Title>()
                .ForMember(dest => dest.Title1, opt => opt.MapFrom(src => src.title)) // title -> Title1 (Kritik)
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.type))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.price))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.notes))
                .ForMember(dest => dest.Royalty, opt => opt.MapFrom(src => src.royalty))
                // Güncellenmemesi gereken alanları korumaya alıyoruz
                .ForMember(dest => dest.TitleId, opt => opt.Ignore())
                .ForMember(dest => dest.Pubdate, opt => opt.Ignore())
                .ForMember(dest => dest.PubId, opt => opt.Ignore())
                .ForMember(dest => dest.Advance, opt => opt.Ignore())
                .ForMember(dest => dest.YtdSales, opt => opt.Ignore())
                .ForMember(dest => dest.Pub, opt => opt.Ignore())
                .ForMember(dest => dest.Sales, opt => opt.Ignore())
                .ForMember(dest => dest.Titleauthors, opt => opt.Ignore());

            // Author mappings
            CreateMap<UpdateAuthorDto, Author>()
                .ForMember(dest => dest.AuId, opt => opt.MapFrom(src => src.Id)) // 'Id' yerine 'id' (DTO'na göre kontrol et)
                .ForMember(dest => dest.AuLname, opt => opt.MapFrom(src => src.AuLName))
                .ForMember(dest => dest.AuFname, opt => opt.MapFrom(src => src.AuFname))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
                .ForMember(dest => dest.Contract, opt => opt.MapFrom(src => src.Contract))
                .ForMember(dest => dest.State, opt => opt.Ignore())
                .ForMember(dest => dest.Zip, opt => opt.Ignore())
                .ForMember(dest => dest.Titleauthors, opt => opt.Ignore());
        }
    }
}