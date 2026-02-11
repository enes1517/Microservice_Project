using Shared.Models;

namespace ProductService.Infastructure.Extensions
{
    public static class TitleFilterExtensions
    {
        public static IQueryable<Title> FilteredByTitle(this IQueryable<Title> titles, string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return titles;

            return titles.Where(t => t.Title1 != null && t.Title1.ToLower().Contains(title.ToLower()));
        }

        public static IQueryable<Title> FilteredByType(this IQueryable<Title> titles, string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return titles;

            return titles.Where(t => t.Type != null && t.Type.ToLower().Contains(type.ToLower()));
        }

        public static IQueryable<Title> FilteredByPrice(this IQueryable<Title> titles, decimal? price)
        {
            if (price is null)
                return titles;

            return titles.Where(t => t.Price == price);
        }

        public static IQueryable<Title> toPaginate(this IQueryable<Title> titles, int pageNumber, int pageSize)
        {
             // Update pageNumber if less than 1
            if (pageNumber < 1) pageNumber = 1;

            return titles.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
