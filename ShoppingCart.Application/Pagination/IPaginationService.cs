using ShoppingCart.Domain;

namespace ShoppingCart.Application.Pagination
{
    public interface IPaginationService
    {
        PaginationResult<T> Paginate<T>(IEnumerable<T> items, int page, int pageSize);
    }
}
