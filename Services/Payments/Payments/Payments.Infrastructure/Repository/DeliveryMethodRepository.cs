using Payments.Domain;

namespace Payments.Infrastructure.Repository;
public class DeliveryMethodRepository : IDeliveryMethodRepository

{
	DeliveryMethod IDeliveryMethodRepository.GetDeliveryMethod(Guid deliveryMethodId)
	{
		var deliveryMethod = new DeliveryMethod
		{
			ShippingPrice = 20,
			DeliveryMethodId = deliveryMethodId,
			DeliveryMethodName = "Online"
		};

		return deliveryMethod;
	}
}
