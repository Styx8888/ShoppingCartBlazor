using ShoppingCart.Domain;

namespace ShoppingCart.Application.Pagination
{
    public class PaginationService : IPaginationService
    {
        public PaginationResult<T> Paginate<T>(IEnumerable<T> items, int page, int pageSize)
        {
            var paginatedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PaginationResult<T>(paginatedItems, items.Count(), page, pageSize);
        }
    }
}
