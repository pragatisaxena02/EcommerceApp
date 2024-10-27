using Microsoft.AspNetCore.Mvc;
using Payments.Application.Services.PaymentService;
using Payments.Domain;

namespace Payments.Controllers;
public class PaymentController : Controller
{
	private IPaymentService _paymentService { get; set; }
	public PaymentController( IPaymentService paymentService )
	{
		_paymentService = paymentService;
	}

	[HttpPost("{basketId}")]
	public async Task<ActionResult<CustomerBasket>> CreateOrUpdateIntent( Guid basketId )
	{
		 var result = await _paymentService.CreateOrUpdatePaymentIntent( basketId).ConfigureAwait( false);

		return Ok(result);
	}
}
