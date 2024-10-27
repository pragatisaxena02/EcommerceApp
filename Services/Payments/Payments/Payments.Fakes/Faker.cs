using Payments.Domain;

namespace Payments.Fakes;

public static class Faker
{
	public static CustomerBasket GetCustomerBasket( Guid? basketId)
	{
		ProductItem[] products = [
		new ProductItem
		{
			ProductId = Guid.NewGuid(),
			Price = 42,
			ProductName = " Tu Clothing Midi dress",
			Quantity = 1
		}
	];

		var deliveryMethodIds = Guid.NewGuid();
		var basket = new CustomerBasket(basketId, deliveryMethodIds, products);

		return basket;
	}
}
