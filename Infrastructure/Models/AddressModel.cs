namespace Infrastructure.Models;

public class AddressModel
{
	public string AddressId { get; set; } = null!;
	public string Street { get; set; } = null!;
	public string City { get; set; } = null!;
	public string State { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string ZipCode { get; set; } = null!;
	public string CountryCallingCode { get; set; } = null!;
	public string Country { get; set; } = null!;
}
