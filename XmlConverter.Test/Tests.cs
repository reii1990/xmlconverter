using Microsoft.Extensions.Logging;

namespace XmlConverter.Test;

public class Tests
{
    private Converter _converter;

    [SetUp]
    public void Setup()
    {
        using var loggerFactory = LoggerFactory.Create(builder => {});
        _converter = new Converter(loggerFactory.CreateLogger<Tests>());
    }

    [Test]
    public void TestBasicInput()
    {
        _converter.Convert("input/basic", "output/basic.xml");
    }

    [Test]
    public void TestUnexpectedLineTypes()
    {
        _converter.Convert("input/unexpectedLineTypes", "output/unexpectedLineTypes.xml");
    }

    [Test]
    public void TestUnexpectedFirstLineType()
    {
        _converter.Convert("input/unexpectedFirstLineType", "output/unexpectedFirstLineType.xml");
    }

    [Test]
    public void TestJustPeople()
    {
        _converter.Convert("input/justPeople", "output/justPeople.xml");
    }

    [Test]
    public void TestJustPeopleWithFamily()
    {
        _converter.Convert("input/justPeopleWithFamily", "output/justPeopleWithFamily.xml");
    }
}