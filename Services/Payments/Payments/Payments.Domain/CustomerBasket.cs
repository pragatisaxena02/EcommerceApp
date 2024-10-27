using Payments.Domain.Enums;

namespace Payments.Domain;

    public class CustomerBasket
    {
        public Guid Id {  get; set; }
	    public CustomerBasket( Guid? basketId, Guid deliveryMethodId, ProductItem[] items )
					{
						Id = basketId ?? Guid.NewGuid();
		        Items = items;
	        	DeliveryMethodId = deliveryMethodId;
					}
	public Guid DeliveryMethodId { get; set; }
	public string? ClientSecret {  get; set; }
	public string? PaymentIntentId { get; set; }
	public ProductItem[] Items { get; set; }
}
