namespace Product.Domain.Query
{
    public class FilterQuery
    {
        public string[]? Brands { get; set; }
        public string[]? Types { get; set; }
        public string? Sort { get; set; }
        public string? Search { get; set; }
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
    }
}
