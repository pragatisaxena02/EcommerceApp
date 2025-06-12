using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Caching.Distributed;
using Product.Domain.Entities;
using Products.Contracts;
using StackExchange.Redis;
using System.Globalization;
using System.Text.Json;

namespace Products.Services
{
    public class CartService : ICartService
    {
        private readonly IDistributedCache _distributedCache;

        public CartService(IDistributedCache distributedCache)
        {
           _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }
        public async Task DeleteCartAsync(string key, CancellationToken cancellationToken)
        {
             await _distributedCache.RemoveAsync(key, cancellationToken).ConfigureAwait( false );
        }

        public async Task<ShoppingCart?> GetCartAsync(string key, CancellationToken cancellationToken )
        {
            var cartData = await _distributedCache.GetStringAsync(key, cancellationToken).ConfigureAwait( false );
            if( cartData == null)
            {
                return null;
            }

            return System.Text.Json.JsonSerializer.Deserialize<ShoppingCart>(cartData);
                
        }

        public async Task<ShoppingCart?> SetCartAsync(ShoppingCart cart, CancellationToken cancellationToken, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(10)
            };

           await _distributedCache.SetStringAsync(cart.Id,
                JsonSerializer.Serialize(cart), options, cancellationToken).ConfigureAwait( false );
            return cart;
        }
    }
}
