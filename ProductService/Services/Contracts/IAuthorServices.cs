using Shared.AuthorDtos;
using Shared.Models;
using Shared.RequestParameters;

namespace ProductService.Services.Contracts
{
    public interface IAuthorServices
    {
        Task<AuthorCreateDto> AuthorCreateAsync(AuthorCreateDto dto);
        Task<bool> AuthorUpdateAsync(UpdateAuthorDto dto);
        Task<bool> AuthorDeleteAsync(string id);
        Task<Author?> GetAuthorByIdAsync(string id);
        Task<List<AuthorWiewDto>> GetListAsync(int n);
        Task<List<Author>> GetAllAuthorsAsync(AuthorRequestParameters p);

    }
}
