using System.Xml;
using XmlConverter.Models;

namespace XmlConverter.Helpers;

public static class XmlExtensions
{
    public static void Write(this Person person, XmlWriter xmlWriter) 
    {
        xmlWriter.WriteStartElement("person");

        if (person.FirstName != null)
        {
            xmlWriter.WriteStartElement("firstname");
            xmlWriter.WriteString(person.FirstName);
            xmlWriter.WriteEndElement();
        }

        if (person.LastName != null) 
        {
            xmlWriter.WriteStartElement("lastname");
            xmlWriter.WriteString(person.LastName);
            xmlWriter.WriteEndElement();
        }

        person.Address?.Write(xmlWriter);
        person.Phone?.Write(xmlWriter);
        foreach (var familyMember in person.Family)
        {
            familyMember.Write(xmlWriter);
        }

        xmlWriter.WriteEndElement();

        xmlWriter.Flush();
    }

    public static void Write(this Phone phone, XmlWriter xmlWriter) 
    {
        xmlWriter.WriteStartElement("phone");

        if (phone.Mobile != null)
        {
            xmlWriter.WriteStartElement("mobile");
            xmlWriter.WriteString(phone.Mobile);
            xmlWriter.WriteEndElement();
        }

        if (phone.Landline != null)
        {
            xmlWriter.WriteStartElement("landline");
            xmlWriter.WriteString(phone.Landline);
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();

        xmlWriter.Flush();
    }

    public static void Write(this Address address, XmlWriter xmlWriter) 
    {
        xmlWriter.WriteStartElement("address");

        if (address.Street != null)
        {
            xmlWriter.WriteStartElement("street");
            xmlWriter.WriteString(address.Street);
            xmlWriter.WriteEndElement();
        }

        if (address.City != null)
        {
            xmlWriter.WriteStartElement("city");
            xmlWriter.WriteString(address.City);
            xmlWriter.WriteEndElement();
        }

        if (address.Zip != null)
        {
            xmlWriter.WriteStartElement("zip");
            xmlWriter.WriteString(address.Zip);
            xmlWriter.WriteEndElement();
        }

        xmlWriter.WriteEndElement();

        xmlWriter.Flush();
    }

    public static void Write(this Family family, XmlWriter xmlWriter) 
    {
        xmlWriter.WriteStartElement("family");

        if (family.Name != null)
        {
            xmlWriter.WriteStartElement("name");
            xmlWriter.WriteString(family.Name);
            xmlWriter.WriteEndElement();
        }

        if (family.Born != null)
        {
            xmlWriter.WriteStartElement("born");
            xmlWriter.WriteString(family.Born);
            xmlWriter.WriteEndElement();
        }

        family.Address?.Write(xmlWriter);
        family.Phone?.Write(xmlWriter);

        xmlWriter.WriteEndElement();

        xmlWriter.Flush();
    }
}