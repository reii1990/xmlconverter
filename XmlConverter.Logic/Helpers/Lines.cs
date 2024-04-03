namespace XmlConverter.Logic.Helpers;

public class Lines
{
    public int CurrentLine { get; set; } = 1;
    private IEnumerable<string> _lines;

    public Lines(IEnumerable<string> lines)
    {
        _lines = lines;
    }

    public string[]? GetCurrentColumns()
    {
        return _lines.FirstOrDefault()?.Split("|");
    }

    public Lines Next()
    {
        CurrentLine++;
        _lines = _lines.Skip(1);
        return this;
    }

    public bool Any()
    {
        return _lines.Any();
    }
}