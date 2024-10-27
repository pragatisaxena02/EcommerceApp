namespace WebApplication1.Models
{
    public class ProductEntity
    {
        public string Name { get; set; } = string.Empty;
        public long Quantity { get; set; }
        public long Rate { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
