using AutoMapper;
using Shared.AuthorDtos;
using Shared.Models;
using Shared.TitleDtos;

namespace Shared.Mapper
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
