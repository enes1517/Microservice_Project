using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Infastructure.Extensions;
using ProductService.Services.Contracts;
using Shared.Models;
using Shared.RequestParameters;
using Shared.TitleDtos;

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
            
            var generatedId = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            
            var titleDto = new Title
            {
                TitleId = generatedId,
                Title1 = title.Title,
                Price = title.Price,
                Notes = title.Notes,
                Type = title.Type,
                Royalty = title.Royalty,
                Pubdate = title.Pubdate,
            };
            
            await _context.Titles.AddAsync(titleDto);
            await _context.SaveChangesAsync();
            
            return title;
        }

        public async Task<(List<TitleDto> Titles, int TotalCount)> GetAllTitlesAsync(TitleRequestParameters p)
        {
            var query = _context.Titles
                .FilteredByTitle(p.Title)
                .FilteredByType(p.Type)
                .FilteredByPrice(p.Price);

            var count = await query.CountAsync();

            var list = await query
                .toPaginate(p.pageNumber, p.pageSize)
                .Select(t=>new TitleDto
                {
                    id=t.TitleId,
                    notes=t.Notes,
                    price=t.Price,
                    type=t.Type,
                    title=t.Title1,
                    royalty=t.Royalty

                })
                .ToListAsync();

            return (list, count);
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
                    id = t.TitleId,
                    type = t.Type,
                    price = t.Price,
                    notes = t.Notes,
                    royalty = t.Royalty,
                    title = t.Title1
                })
                .ToListAsync(); 
        }

        public async Task<TitleDto?> GetByIdAsync(string id)
        {
            var title = await _context.Titles.FindAsync(id);
            if (title is null)
                return null;
            
            return new TitleDto
            {
                id = title.TitleId,
                type = title.Type,
                price = title.Price,
                notes = title.Notes,
                royalty = title.Royalty,
                title = title.Title1
            };
        }

        public async Task<bool> UpdateAsync(UpdateTitleDto updateTitle)
        {
        
            var exist = await _context.Titles.FindAsync(updateTitle.id);
            if (exist is null)
                return false;

            
            _mapper.Map(updateTitle, exist);

            
            _context.Titles.Update(exist);
            var result = await _context.SaveChangesAsync();

            
            return result > 0;
        }
    }
}
