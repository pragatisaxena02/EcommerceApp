using Payments.Domain;

namespace Payments.Infrastructure.Repository;
public interface IDeliveryMethodRepository
{
	DeliveryMethod GetDeliveryMethod(Guid deliveryMethodId);
}
