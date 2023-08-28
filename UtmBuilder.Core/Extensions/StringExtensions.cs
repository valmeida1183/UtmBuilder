namespace UtmBuilder.Core.Extensions;

public static class StringExtensions
{
    public static (string Key, string Value) QueryStringTuple(this string queryString)
    {
        var result = queryString.Split("=");
        return result.Length == 1 ? (result[0], string.Empty) : (result[0], result[1]);
    }
}