namespace Products.Domain
{
    public class AddressDto
    {
        public int Id { get; set; }  // <-- Primary Key

        public string Line1 { get; set; } = string.Empty;
        public string? Line2 { get; set; } = string.Empty;
        public  string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}