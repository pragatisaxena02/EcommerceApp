using Payments.Domain;

namespace Payments.Application.Services.PaymentService;

public interface IPaymentService
{
	 Task<CustomerBasket> CreateOrUpdatePaymentIntent(Guid basketId);
}
