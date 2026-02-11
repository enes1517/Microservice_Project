using Refit;
using Shared.AuthorDtos;
using Shared.RequestParameters;
using Shared.TitleDtos;

namespace OrchestratorService.Refit
{
    public interface IProductServiceApi
    {
        // Query parametrelerini tek bir nesne üzerinden gönderiyoruz
        [Get("/api/Products")]
        Task<IApiResponse<object>> GetAllTitlesAsync([Query] TitleRequestParameters p);

        [Get("/api/Products/{id}")]
        Task<IApiResponse<object>> GetProductByIdAsync(string id);

        [Post("/api/Products")]
        Task<IApiResponse<object>> CreateProductAsync([Body] CreateTitleDto dto);

        [Put("/api/Products")]
        Task<IApiResponse<object>> UpdateProductAsync([Body] UpdateTitleDto dto);

        [Delete("/api/Products/{id}")]
        Task<IApiResponse<object>> DeleteProductAsync(string id);

        // Authors için de [Query] ekliyoruz
        [Get("/api/Authors")]
        Task<IApiResponse<object>> GetAllAuthorsAsync([Query] AuthorRequestParameters p);

        [Get("/api/Authors/{id}")]
        Task<IApiResponse<object>> GetAuthorByIdAsync(string id);

        [Post("/api/Authors")]
        Task<IApiResponse<object>> CreateAuthorAsync([Body] AuthorCreateDto dto);

        [Put("/api/Authors")]
        Task<IApiResponse<object>> UpdateAuthorAsync([Body] UpdateAuthorDto dto);

        [Delete("/api/Authors/{id}")]
        Task<IApiResponse<object>> DeleteAuthorAsync(string id);
    }
}