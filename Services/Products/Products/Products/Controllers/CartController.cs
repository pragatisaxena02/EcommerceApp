using Microsoft.AspNetCore.Mvc;
using Product.Domain.Query;
using Product.Domain;
using Products.Contracts;
using Product.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Products.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IDistributedCache _distributedCache;

        public CartController(ICartService cartService, IDistributedCache distributeCache)
        {
            _cartService = cartService;
            _distributedCache = distributeCache;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById( string id, CancellationToken cancellationToken )
        {
           var cart = await _cartService.GetCartAsync( id, cancellationToken ).ConfigureAwait( false );
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart, CancellationToken cancellationToken)
        {
            try
            {
                var updatedCart = await _cartService.SetCartAsync(cart, cancellationToken).ConfigureAwait(false);

                return Ok(updatedCart);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here for brevity)
                throw ex;
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart( string id, CancellationToken cancellationToken )
        {
            await _cartService.DeleteCartAsync( id, cancellationToken).ConfigureAwait( false );
            
            return Ok();
        }
    }
}
