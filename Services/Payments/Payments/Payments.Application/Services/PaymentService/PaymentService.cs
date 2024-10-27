using Microsoft.Extensions.Configuration;
using Payments.Domain;
using Payments.Domain.Enums;
using Payments.Infrastructure.Repository;
using Stripe;
namespace Payments.Application.Services.PaymentService;
public class PaymentService : IPaymentService
{
	private IConfiguration _configuration { get; set; }
	private IBasketRepository _basketRepository { get; set; }
	private IDeliveryMethodRepository _deliveryMethodRepository { get; set; }
	public PaymentService( IConfiguration configuration, 
		                     IBasketRepository basketRepository,
												 IDeliveryMethodRepository deliveryMethodRepository)
	{
		_configuration = configuration;
		_basketRepository = basketRepository;
		_deliveryMethodRepository = deliveryMethodRepository;
	}
	public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(Guid basketId)
	{
		StripeConfiguration.ApiKey = _configuration["StripeSettings:SecretKey"];
		var basket = _basketRepository.GetCustomerBasket(basketId);

		var deliveryMethod = _deliveryMethodRepository.GetDeliveryMethod(basket.DeliveryMethodId);

		if (deliveryMethod is null)
			throw new Exception(" delivery method not found ");
		
		var shippingPrice = deliveryMethod.ShippingPrice;

		var service = new PaymentIntentService();

		if( basket.PaymentIntentId is null )
		{
			var options = new PaymentIntentCreateOptions
			{
				Amount = basket.Items.Sum(item => item.Quantity * item.Price*100) + shippingPrice,
				Currency = CurrencyType.GBP.ToString(),
				//AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
				//{
				//	Enabled = true,
				//}
				PaymentMethodTypes = new List<string> { "card" }
			};

		  var intent = await service.CreateAsync(options);
			basket.PaymentIntentId = intent.Id;
			basket.ClientSecret = intent.ClientSecret;
		}
		else
		{
			var options = new PaymentIntentUpdateOptions
			{
				Amount = basket.Items.Sum(item => item.Quantity * item.Price) + shippingPrice
			};

			await service.UpdateAsync( basket.PaymentIntentId, options );
			return basket;
		}

	//	return _basketRepository.UpdateCustomerBasket( basket );
		return basket;
	}
}
