namespace Payments.Domain;
public class DeliveryMethod
{
	public Guid DeliveryMethodId { get; set; }
	public string DeliveryMethodName { get; set; }
	public int ShippingPrice {  get; set; }
}
