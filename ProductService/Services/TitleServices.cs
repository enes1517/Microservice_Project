using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Services.Contracts;

namespace ProductService.Services
{
    public class TitleServices : ITitleServices
    {
        private readonly PubsContext _context;
        private readonly IMapper _mapper;

        public TitleServices(PubsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateTitleDto> CreateTitle(CreateTitleDto title)
        {
            var titleDto = new Title
            {
                TitleId=title.Id,
                Title1=title.Title,
                Price=title.Price,
                Notes=title.Notes,
                Type=title.Type,
                Royalty=title.Royalty,
                Pubdate=title.Pubdate,
        
               
                
            };
            await _context.Titles.AddAsync(titleDto);
            await _context.SaveChangesAsync();
            return title;

        }

        public async Task<bool> DeleteAsync(string id)
        {
            var entity = await _context.Titles.FindAsync(id);
            if(entity is null)
                return false;

             _context.Titles.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TitleDto>> GetAllAsync(int n)
        {
            return await _context.Titles
                .Take(n)
                .Select(t => new TitleDto 
                {
                    Id = t.TitleId,
                    Type = t.Type,
                    Price = t.Price,
                    Notes = t.Notes,
                    Royalty = t.Royalty,
                    Title = t.Title1
                })
                .ToListAsync(); 
        }

        public async Task<bool> UpdateAsync(UpdateTitleDto updateTitle)
        {
            var exist = await _context.Titles.FindAsync(updateTitle.Id);
            if(exist is null)
                return false;
           _mapper.Map(updateTitle, exist); 

            _context.Titles.Update(exist);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
