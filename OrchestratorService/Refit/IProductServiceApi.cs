using Refit;
using Shared.AuthorDtos;
using Shared.TitleDtos;

namespace OrchestratorService.Refit
{
    public interface IProductServiceApi
    {
        // --- Products (ProductsController) ---
        [Get("/api/Products")]
        Task<IApiResponse<object>> GetAllProductsAsync([Query] int n);

        [Get("/api/Products/{id}")]
        Task<IApiResponse<object>> GetProductByIdAsync(string id);

        [Post("/api/Products")]
        Task<IApiResponse<object>> CreateProductAsync([Body] CreateTitleDto dto);

        [Put("/api/Products")]
        Task<IApiResponse<object>> UpdateProductAsync([Body] UpdateTitleDto dto);

        [Delete("/api/Products/{id}")]
        Task<IApiResponse<object>> DeleteProductAsync(string id);

        // --- Authors (AuthorsController) ---
        [Get("/api/Authors")]
        Task<IApiResponse<object>> GetAllAuthorsAsync([Query] int n);

        [Post("/api/Authors")]
        Task<IApiResponse<object>> CreateAuthorAsync([Body] AuthorCreateDto dto);

        [Put("/api/Authors")]
        Task<IApiResponse<object>> UpdateAuthorAsync([Body] UpdateAuthorDto dto);

        [Delete("/api/Authors/{id}")]
        Task<IApiResponse<object>> DeleteAuthorAsync(string id);
    }
}