using Payments.Domain;

namespace Payments.Infrastructure.Repository;

public interface IBasketRepository
{
		CustomerBasket GetCustomerBasket(Guid basketId);
		CustomerBasket UpdateCustomerBasket(CustomerBasket basket);
}

