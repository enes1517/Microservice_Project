using Shared.Models;

namespace ProductService.Infastructure.Extensions
{
    public static class TitleFilterExtensions
    {
       public static IQueryable<Title> FilteredByTitle(this IQueryable<Title> query , string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return query;

            return query.Where(t=>t.Title1.ToLower().Contains(title.ToLower()));

            
        }
        public static IQueryable<Title> FilteredByType(this IQueryable<Title> query, string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return query;

            return query.Where(t => t.Type.ToLower().Contains(type.ToLower()));


        }
        public static IQueryable<Title> FilteredByPrice(this IQueryable<Title> query, decimal? price)
        {
            if (price is null)
                return query;

            return query.Where(t => t.Price.Equals(price));


        }
        public static IQueryable<Title> toPaginate(this IQueryable<Title> query, int pageNumber, int pageSize)
        {
            return query.Skip((pageNumber-1)*pageSize)
                .Take(pageSize);
        }
    }
}
