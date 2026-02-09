using Shared.Models;

namespace ProductService.Infastructure.Extensions
{
    public static class AuthorFilterExtensions
    {
        public static IQueryable<Author> FiltredByLocation(this IQueryable<Author> authors, string? location )
        {
            if ( string.IsNullOrWhiteSpace(location ))
                return authors;

            return authors.Where(lct=>lct.City.ToLower().Contains(location.ToLower()));
               
        }
        public static IQueryable<Author> FiltredByFullname(this IQueryable<Author> authors, string? flname)
        {
            if (string.IsNullOrWhiteSpace(flname))
                return authors;

            return authors.Where(fln => fln.AuFname.ToLower().Contains(flname.ToLower())|| fln.AuLname.ToLower().Contains(flname.ToLower()));

        }
        public static IQueryable<Author> toPaginate(this IQueryable<Author> authors,int pageNumber,int pageSize)
        {
            return authors.Skip((pageNumber-1)*pageSize)
                .Take(pageSize);
        }

    }
}
