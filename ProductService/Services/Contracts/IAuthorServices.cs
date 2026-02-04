using ProductService.Dtos.AuthorDtos;

namespace ProductService.Services.Contracts
{
    public interface IAuthorServices
    {
        Task<AuthorCreateDto> AuthorCreateAsync(AuthorCreateDto dto);
        Task<bool> AuthorUpdateAsync(UpdateAuthorDto dto);
        Task<bool> AuthorDeleteAsync(string id);
        Task<List<AuthorWiewDto>> GetListAsync(int n);

    }
}
