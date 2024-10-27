namespace Payments.Domain;
public class ProductItem
{
	public required Guid ProductId { get; set; }
	public string? ProductName { get; set; }
	public int Price { get; set; }
	public int Quantity { get; set; }
}
