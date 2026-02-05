using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Services.Contracts;
using Shared.AuthorDtos;
using Shared.Models;

namespace ProductService.Services
{
    public class AuthorServices :IAuthorServices
    {
        private readonly PubsContext _context;
        private readonly IMapper _mapper;

        public AuthorServices(PubsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<AuthorCreateDto> AuthorCreateAsync(AuthorCreateDto dto)
        {
            var author = new Author()
            {
                AuId = dto.AuId,
                AuFname = dto.AuFname,
                Address = dto.Address,
                AuLname = dto.AuLname,
                Phone = dto.Phone,
                City = dto.City,
                Contract = true,

            };

            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return dto;
           
        }

        public async Task<bool> AuthorUpdateAsync(UpdateAuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(dto.AuId);
            if (author is null)
                return false;
            
            _mapper.Map(dto,author);
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AuthorDeleteAsync(string id)
        {
            var author= await _context.Authors.FindAsync(id);
            if(author is null)
                return false;

           _context.Authors.Remove(author);
          await  _context.SaveChangesAsync();
            return true;

        }

        public async Task<List<AuthorWiewDto>> GetListAsync(int n)
        {
            return await _context.Authors.
                Take(n).Select(a => new AuthorWiewDto
                {
                    FullName = $"{a.AuFname} { a.AuLname}",
                    Id=a.AuId,
                    Location=a.City,
                    Phone=a.Phone,
                    BookCount=a.Titleauthors.Count(),   

                }).ToListAsync();
                
        }
    }
}
