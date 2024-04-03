namespace XmlConverter.Models;

public class Family
{
    public string? Name { get; set; }
    public string? Born { get; set; }
    public Address? Address { get; set; }
    public Phone? Phone { get; set; }

    public Family(string? name, string? born)
    {
        Name = name;
        Born = born;
    }
}