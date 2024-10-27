using Payments.Domain;
using Payments.Fakes;

namespace Payments.Infrastructure.Repository;
public class BasketRepository : IBasketRepository
{
	public CustomerBasket GetCustomerBasket(Guid basketId)
	{
	
		var basket = Faker.GetCustomerBasket(basketId);
		return basket;
	}

	public CustomerBasket UpdateCustomerBasket(CustomerBasket basket)
	{
		return basket;
	}
}
