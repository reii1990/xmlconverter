namespace XmlConverter.Models;

public class Phone
{
    public string? Mobile { get; set; }
    public string? Landline { get; set; }

    public Phone(string? mobile, string? landline)
    {
        Mobile = mobile;
        Landline = landline;
    }
}