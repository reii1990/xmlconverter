namespace XmlConverter.Models;

public class Address
{
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? Zip { get; set; }

    public Address(string? street, string? city, string? zip)
    {
        Street = street;
        City = city;
        Zip = zip;
    }
}