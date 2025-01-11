namespace ASP_NET_MVC_Shop_Example.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Product> Products { get; set; } = null!;
        public SortState SortState { get; set; } = null!;
        public FilterState FilterState { get; set; } = null!;
    }
}
