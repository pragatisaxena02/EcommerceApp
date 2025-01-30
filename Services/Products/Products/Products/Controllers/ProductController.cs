using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Product.Domain;
using Product.Domain.Query;
using System.Data.SqlTypes;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public Pagination<Product.Domain.Product> GetProducts([FromQuery] FilterQuery? query)
        {
            var path = _configuration["AppSettings:BaseUrl"];
            var products = GetProducts(path!, query);
            var paginatedResponse = new Pagination<Product.Domain.Product>
            {
                PageIndex =query.pageIndex,
                PageSize = query.pageSize,
                Count = products.Item2,
                Data = products.Item1
            };
            return paginatedResponse;  // Return 200 OK with the list of products
        }

        [HttpGet("brands")]
        public string[] GetBrands()
        {
            return ["TechBrand", "PhoneCo", "SoundMax", "Samsung", "HomeTech"];
        }

        [HttpGet("types")]
        public string[] GetTypes()
        {
            return ["Electronics", "Accessories", "Home Appliances", "Clothes"];
        }
        private static (Product.Domain.Product[], int) GetProducts( string path, FilterQuery? query )
        {
            Product.Domain.Product[] products = new[]
         {
            new Product.Domain.Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "A high-performance laptop for work and gaming.",
                Price = 999.99m,
                ImageUrl = path+"/Laptop.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 25
            },
            new Product.Domain.Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "Latest model with cutting-edge features.",
                Price = 799.99m,
                ImageUrl = path+"/Phone.jpg",
                Type = "Electronics",
                Brand = "PhoneCo",
                QuantityInStock = 50
            },
            new Product.Domain.Product
            {
                Id = 3,
                Name = "Headphones",
                Description = "Noise-cancelling wireless headphones.",
                Price = 149.99m,
                ImageUrl = path+"/Headphone.jpg",
                Type = "Accessories",
                Brand = "SoundMax",
                QuantityInStock = 100
            },
            new Product.Domain.Product
            {
                Id = 4,
                Name = "Coffee Maker",
                Description = "Automatic coffee machine with a sleek design.",
                Price = 89.99m,
                ImageUrl = path+"/CoffeeMaker.jpg",
                Type = "Home Appliances",
                Brand = "HomeTech",
                QuantityInStock = 30
            },
            new Product.Domain.Product
            {
                Id = 5,
                Name = "Washing Machine",
                Description = "Front-loading washing machine with energy-efficient features.",
                Price = 499.99m,
                ImageUrl =  path+"/WashingMachine.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 15
            },
            new Product.Domain.Product
            {
                Id = 6,
                Name = "Bluetooth Speaker",
                Description = "Portable Bluetooth speaker with deep bass and long battery life.",
                Price = 59.99m,
                ImageUrl =path+"/BluetoothSpeaker.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            },
            new Product.Domain.Product
            {
                Id = 7,
                Name = "Oven",
                Description = "Oven electronics",
                Price = 59.99m,
                ImageUrl =path+"/oven.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            },
            new Product.Domain.Product
            {
                Id = 8,
                Name = "Microwave",
                Description = "Microwave electronics",
                Price = 59.99m,
                ImageUrl =path+"/microwave.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            },
            new Product.Domain.Product
            {
                Id = 9,
                Name = "Refigerator",
                Description = "Refigerator electronics",
                Price = 59.99m,
                ImageUrl =path+"/Refigerator.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            },
            new Product.Domain.Product
            {
                Id = 10,
                Name = "Short Refigerator",
                Description = "Short Refigerator electronics",
                Price = 59.99m,
                ImageUrl =path+"/ShortRefigerator.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            },
            new Product.Domain.Product
            {
                Id = 11,
                Name = "POS",
                Description = "Point of sale",
                Price = 59.99m,
                ImageUrl =path+"/pos.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 80
            }
        };
            if(query is null )            
              return (products, products.Length);

            if (query is not null && query.Brands is null && query.Types is null)
            {
                if (query.Sort is null)
                {
                    return (products, products.Length);
                }

                if (query.Sort.Equals( "name"))
                {
                   products = products.OrderBy( x => x.Name ).ToArray();
                }
                else if (query.Sort.Equals("priceDesc"))
                {
                    products = products.OrderByDescending(x => x.Price).ToArray();
                }
                else if (query.Sort.Equals("priceAsc"))
                {
                    products = products.OrderBy(x => x.Price).ToArray();
                }

                if (query.pageSize != 0)
                {
                    var paginatedResponse =  products.Skip(query.pageIndex).Take(query.pageSize);
                    if( !string.IsNullOrEmpty( query.Search))
                    {
                        paginatedResponse = paginatedResponse.Where(x => x.Name.IndexOf(query.Search) != -1).ToArray();
                    }
                    return (paginatedResponse.ToArray(),products.Length);
                }

                if( query.Search != null)
                {
                    products = products.Where(x => x.Name.IndexOf(query.Search) != -1).ToArray();
                }
                return (products, products.Length);

            }
            var filteredProducts = products.Where(x => (query.Brands !=null && query.Brands[0]!="all" &&query.Brands.Any()) ? query.Brands.Contains(x.Brand) : true && (x.Type != null && query.Types[0] != "all" && x.Type.Any()) ? query.Types!.Contains(x.Type) : true).ToArray();

            if (query.pageSize != 0)
            {
                var pagedFilterProducts = filteredProducts.Skip(query.pageIndex* query.pageSize).Take(query.pageSize).ToArray();
                pagedFilterProducts = pagedFilterProducts.Where(x => x.Name.IndexOf(query.Search) != -1).ToArray();

                return (pagedFilterProducts, products.Length);
            }

            
            return (products, products.Length);
        }

        private static readonly Product.Domain.Product[]Products = new[]
        {
            new Product.Domain.Product
            {
                Id = 1,
                Name = "Laptop",
                Description = "A high-performance laptop for work and gaming.",
                Price = 999.99m,
                ImageUrl = "/Laptop2.jpg",
                Type = "Electronics",
                Brand = "TechBrand",
                QuantityInStock = 25
            },
            new Product.Domain.Product
            {
                Id = 2,
                Name = "Smartphone",
                Description = "Latest model with cutting-edge features.",
                Price = 799.99m,
                ImageUrl = "http://example.com/smartphone.jpg",
                Type = "Electronics",
                Brand = "PhoneCo",
                QuantityInStock = 50
            },
            new Product.Domain.Product
            {
                Id = 3,
                Name = "Headphones",
                Description = "Noise-cancelling wireless headphones.",
                Price = 149.99m,
                ImageUrl = "/Images/Laptop1.jpg",
                Type = "Accessories",
                Brand = "SoundMax",
                QuantityInStock = 100
            },
            new Product.Domain.Product
            {
                Id = 4,
                Name = "Coffee Maker",
                Description = "Automatic coffee machine with a sleek design.",
                Price = 89.99m,
                ImageUrl = "http://example.com/coffeemaker.jpg",
                Type = "Home Appliances",
                Brand = "HomeTech",
                QuantityInStock = 30
            },
            new Product.Domain.Product
            {
                Id = 5,
                Name = "Washing Machine",
                Description = "Front-loading washing machine with energy-efficient features.",
                Price = 499.99m,
                ImageUrl = "http://example.com/washingmachine.jpg",
                Type = "Home Appliances",
                Brand = "WashPro",
                QuantityInStock = 15
            },
            new Product.Domain.Product
            {
                Id = 6,
                Name = "Bluetooth Speaker",
                Description = "Portable Bluetooth speaker with deep bass and long battery life.",
                Price = 59.99m,
                ImageUrl = "http://example.com/bluetoothspeaker.jpg",
                Type = "Electronics",
                Brand = "AudioPro",
                QuantityInStock = 80
            }
        };
    }
}
