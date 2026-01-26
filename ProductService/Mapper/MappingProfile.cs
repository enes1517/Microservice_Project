using AutoMapper;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateTitleDto, Title>();
        }
    }
}
