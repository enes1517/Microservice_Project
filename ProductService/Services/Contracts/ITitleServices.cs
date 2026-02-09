using Shared.Models;
using Shared.RequestParameters;
using Shared.TitleDtos;

namespace ProductService.Services.Contracts
{
    public interface ITitleServices
    {
        Task<CreateTitleDto> CreateTitle(CreateTitleDto title);
        Task<bool> UpdateAsync(UpdateTitleDto updateTitle);
        Task<bool> DeleteAsync(string id);
        Task<List<TitleDto>> GetAllAsync(int n);
        Task<TitleDto?> GetByIdAsync(string id);
        Task<List<Title>> GetAllTitlesAsync(TitleRequestParameters p);
    }
}
