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
            // Auto-generate ID (6 character GUID)
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

        public async Task<List<Title>> GetAllTitlesAsync(TitleRequestParameters p)
        {
            return await _context.Titles
                .FilteredByTitle(p.Title)
                .FilteredByType(p.Type)
                .FilteredByPrice(p.Price)
                .toPaginate(p.pageNumber,p.pageSize)
                .ToListAsync();
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
            // 1. Kaydı veritabanından bul
            var exist = await _context.Titles.FindAsync(updateTitle.id);
            if (exist is null)
                return false;

            // 2. Mapping işlemini gerçekleştir (MappingProfile buradaki veriyi exist içine yazar)
            _mapper.Map(updateTitle, exist);

            // 3. EF Core'a bu nesnenin değiştiğini bildir ve kaydet
            _context.Titles.Update(exist);
            var result = await _context.SaveChangesAsync();

            // Değişiklik kaydedildiyse (result > 0) true döner
            return result > 0;
        }
    }
}
