using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.ValueObjects;

public class Url : ValueObject
{
    /// <summary>
    /// Address of URL (Website link)
    /// </summary>
    public string Address { get; }

    /// <summary>
    /// Create a new URL
    /// </summary>
    /// <param name="address"></param>
    public Url(string address)
    {
        Address = address;
        InvalidUrlException.ThrowIfInvalid(address);
    }
}