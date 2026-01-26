using ProductService.Dtos;

namespace ProductService.Services.Contracts
{
    public interface ITitleServices
    {
        Task<CreateTitleDto> CreateTitle(CreateTitleDto title);
        Task<bool> UpdateAsync(UpdateTitleDto updateTitle);
        Task<bool> DeleteAsync(string id);
        Task<List<TitleDto>> GetAllAsync(int n);
    }
}
