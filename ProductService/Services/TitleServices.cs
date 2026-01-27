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
                Type = title.Type,
                Advance=title.Advance,
                Notes=title.Notes,
                Royalty=title.Royalty,
                Pubdate = DateTime.Now 
                
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

        public  async Task<List<TitleDto>> GetAllAsync(int n)
        {
            var entity = await _context.Titles.Take(n).ToListAsync();
            return entity.Select(t => new TitleDto
            {
                Id=t.TitleId,
                Title = t.Title1,
                Type = t.Type,
                Price=t.Price,
                Advance=t.Advance,
                Notes=t.Notes,
                Royalty=t.Royalty
               
            }).ToList();
            
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
