namespace XmlConverter.Helpers;

public static class ArrayExtensions
{
    public static T? GetOrDefault<T>(this T[] array, int index)
    {
        return array.Length > index ? array[index] : default;
    }
}