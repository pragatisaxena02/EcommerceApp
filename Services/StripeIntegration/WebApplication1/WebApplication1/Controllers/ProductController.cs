using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }
        // GET: ProductController/Create

        [HttpGet]
        public IActionResult Index()
        {
            var productList = new List<ProductEntity>()
            {
                new ProductEntity
                {
                    Name = "Product 1",
                    Rate = 1000,
                    Quantity = 1,
                    Image = "img/Image1.png"
                },
                 new ProductEntity
                {
                    Name = "Product 2",
                    Rate = 1200,
                    Quantity = 2,
                    Image = "img/Image2.png"
                }

            };

            return View(productList);
        }

        public IActionResult CheckOut()
        {
            var productList = new List<ProductEntity>()
            {
                new ProductEntity
                {
                    Name = "Product 1",
                    Rate = 1000,
                    Quantity = 1,
                    Image = "img/Image1.png"
                },
                 new ProductEntity
                {
                    Name = "Product 2",
                    Rate = 1200,
                    Quantity = 2,
                    Image = "img/Image2.png"
                }

            };

            var domain = "https://localhost:7170/";

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>()
               ,
                Mode = "payment",
                SuccessUrl = domain + "Product/OrderConfirmation",
                CancelUrl = domain + "Product/Login",
            };

            foreach( var entity in productList)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = entity.Quantity * entity.Rate,
                        Currency = "inr",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = entity.Name
                        }
                    },
                    Quantity = entity.Quantity,
                }) ;
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;
            Response.Headers.Add("Location", session.Url);


            return new StatusCodeResult(303);
        }

        public IActionResult OrderConfirmation()
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if( session.PaymentStatus == "paid")
            {
                return View("Success");
            }

            return View("Login");
        }

        public IActionResult Success()
        {
           return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
