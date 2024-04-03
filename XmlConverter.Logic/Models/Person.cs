using System.Xml;

namespace XmlConverter.Models;

public class Person
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Phone? Phone { get; set; }
    public Address? Address { get; set; }
    public List<Family> Family { get; set; } = new List<Family>();

    public Person(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}