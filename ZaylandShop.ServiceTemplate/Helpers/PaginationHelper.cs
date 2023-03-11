using ZaylandShop.ServiceTemplate.Abstractions;
using ZaylandShop.ServiceTemplate.Models.Pagination;

namespace ZaylandShop.ServiceTemplate.Helpers;

public static class PaginationHelper
{
    public static IPaginationResult<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber)
    {
        return source.AsPagination(pageNumber, PaginationResult<T>.DefaultPageSize);
    }
    
    public static IPaginationResult<T> AsPagination<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
    {
        if(pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(
                "pageNumber", 
                "The page number should be greater than or equal to 1.");
        }

        return new PaginationResult<T>(source.AsQueryable(), pageNumber, pageSize);
    }
}