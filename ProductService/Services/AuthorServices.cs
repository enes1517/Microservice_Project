using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Infastructure.Extensions;
using ProductService.Services.Contracts;
using Shared.AuthorDtos;
using Shared.Models;
using Shared.RequestParameters;

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
            try 
            {
                // Generate random numeric ID: XXX-XX-XXXX (Standard Pubs format)
                var rnd = new Random();
                var p1 = rnd.Next(100, 999); // 3 digits
                var p2 = rnd.Next(10, 99);   // 2 digits
                var p3 = rnd.Next(1000, 9999); // 4 digits
                var generatedId = $"{p1}-{p2}-{p3}";
                
                var author = new Author()
                {
                    AuId = generatedId,
                    AuFname = dto.AuFname,
                    Address = dto.Address,
                    AuLname = dto.AuLname,
                    Phone = dto.Phone,
                    City = dto.City,
                    State = "NA", // Default value
                    Zip = "00000", // Default value
                    Contract = dto.Contract, // Use value from DTO
                };

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
                
                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ AuthorCreateAsync Error: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"❌ Inner Exception: {ex.InnerException.Message}");
                return null;
            }
        }

        public async Task<bool> AuthorUpdateAsync(UpdateAuthorDto dto)
        {
            var author = await _context.Authors.FindAsync(dto.Id);
            if (author is null)
                return false;
            
            _mapper.Map(dto, author);
            await _context.SaveChangesAsync();  
            return true;
        }
        public async Task<(List<AuthorWiewDto> Authors, int TotalCount)> GetAllAuthorsAsync(AuthorRequestParameters r)
        {
            var query = _context.Authors
                .FiltredByLocation(r.Location)
                .FiltredByFullname(r.Fullname);

            var count = await query.CountAsync();

            var list = await query
                .toPaginate(r.pageNumber,r.pageSize)
                .Select(a=>new AuthorWiewDto
                {
                    Id = a.AuId,
                    FullName=$"{a.AuFname} {a.AuLname}",
                    BookCount=a.Titleauthors.Count,
                    Location=a.City,
                    Phone=a.Phone,
                })
                .ToListAsync();

            return (list, count);
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


        public async Task<Author?> GetAuthorByIdAsync(string id)
        {
            return await _context.Authors.FindAsync(id);
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
