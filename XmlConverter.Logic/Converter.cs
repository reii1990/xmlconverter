using System.Xml;
using Microsoft.Extensions.Logging;
using XmlConverter.Helpers;
using XmlConverter.Logic.Helpers;
using XmlConverter.Models;

namespace XmlConverter;

public class Converter
{
    private ILogger _logger;

    public Converter(ILogger logger)
    {
        _logger = logger;
    }

    public void Convert(string inputFile, string outputFile)
    {
        var lines = new Lines(File.ReadLines(inputFile));

        var outputDirectory = Path.GetDirectoryName(outputFile);
        if(outputDirectory != null && outputDirectory != string.Empty)
        {
            Directory.CreateDirectory(outputDirectory);
        }

        using (var xmlWriter = XmlWriter.Create(outputFile))
        {
            xmlWriter.WriteStartElement("people");
            do {
                lines = TryReadNextPerson(lines, out var person);
                if (person != null) 
                {
                    person.Write(xmlWriter);
                }
            } while (lines.Any());
            xmlWriter.WriteEndElement();
            xmlWriter.Flush();
        }
    }

    private Lines TryReadNextPerson(Lines lines, out Person? person)
    {
        try 
        {
            var columns = lines.GetCurrentColumns();

            if (columns?.First() != "P")
            {
                _logger.LogWarning("Expected line {line} to represent a person", lines.CurrentLine);
                person = null;
                return lines.Next();
            }

            person = new Person(columns.GetOrDefault(1), columns.GetOrDefault(2)); 

            return HandlePerson(lines.Next(), person);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred while processing line {line}", lines.CurrentLine);
            person = null;
            return lines.Next();
        }
    } 

    private Lines HandlePerson(Lines lines, Person person)
    {
        try
        {
            var columns = lines.GetCurrentColumns();

            switch(columns?.FirstOrDefault())
            {
                case "T":
                    person.Phone = new Phone(columns.GetOrDefault(1), columns.GetOrDefault(2));
                    break;
                case "A":
                    person.Address = new Address(columns.GetOrDefault(1), columns.GetOrDefault(2), columns.GetOrDefault(3));
                    break;
                case "F":
                    var familyMember = new Family(columns.GetOrDefault(1), columns.GetOrDefault(2));
                    person.Family.Add(familyMember);
                    lines = HandleFamily(lines.Next(), familyMember);
                    return HandlePerson(lines, person);
                case "P":
                case null:
                    return lines;
                    // either a new person or eof so just return lines
                default:
                    _logger.LogWarning("Line {line} had an unexpexted type {type}", lines.CurrentLine, columns?.FirstOrDefault());
                    break;
            }
            return HandlePerson(lines.Next(), person);
        } 
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred while processing line {line}", lines.CurrentLine);
            return lines.Next();
        }
    }

    private Lines HandleFamily(Lines lines, Family family)
    {
        try
        {
            var columns = lines.GetCurrentColumns();

            switch(columns?.FirstOrDefault())
            {
                case "T":
                    family.Phone = new Phone(columns.GetOrDefault(1), columns.GetOrDefault(2));
                    break;
                case "A":
                    family.Address = new Address(columns.GetOrDefault(1), columns.GetOrDefault(2), columns.GetOrDefault(3));
                    break;
                case "F":
                case "P":
                case null:
                    return lines;
                    // either a new person, familymember or eof so just return lines
                default:
                    _logger.LogWarning("Line {line} had an unexpexted type {type}", lines.CurrentLine, columns?.FirstOrDefault());
                    break;
            }
            return HandleFamily(lines.Next(), family);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error occurred while processing line {line}", lines.CurrentLine);
            return lines.Next();
        }
    }
}