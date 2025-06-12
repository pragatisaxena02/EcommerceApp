using Product.Domain.Entities;

namespace Products.Contracts
{
    public interface ICartService
    {
        Task<Product.Domain.Entities.ShoppingCart?> GetCartAsync(string key, CancellationToken cancellationToken);
        Task<ShoppingCart?> SetCartAsync(ShoppingCart cart, CancellationToken cancellationToken, TimeSpan? expiration = null);
        Task DeleteCartAsync(string key, CancellationToken cancellationToken);
    }
}
