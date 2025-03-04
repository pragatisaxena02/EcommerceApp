using System.ComponentModel.DataAnnotations;

namespace Product.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }

        [Required]
        public string? ImageUrl { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Brand { get; set; }
        public int QuantityInStock { get; set; }
    }
}
