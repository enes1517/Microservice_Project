using AutoMapper;
using ProductService.Dtos;
using ProductService.Dtos.AuthorDtos;
using ProductService.Models;

namespace ProductService.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateTitleDto, Title>();
            CreateMap<UpdateAuthorDto, Author>();
        }
    }
}
