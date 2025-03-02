namespace ShoppingCart.Domain
{
    public class PaginationResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public PaginationResult(List<T> data, int totalItems, int page, int pageSize)
        {
            Data = data;
            TotalItems = totalItems;
            Page = page;
            PageSize = pageSize;
        }
    }

}
