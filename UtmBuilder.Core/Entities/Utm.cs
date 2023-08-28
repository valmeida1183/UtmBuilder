using UtmBuilder.Core.Extensions;
using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Entities;

public class Utm
{
    /// <summary>
    /// URL (Website Link)
    /// </summary>
    public Url Url { get; init; } // = null!;

    /// <summary>
    /// Campaign Details
    /// </summary>
    public Campaign Campaign { get; init; } // init só pode ser inicializada no construtor

    public Utm(Url url, Campaign campaign)
    {
        Url = url;
        Campaign = campaign;
    }

    public override string ToString()
    {
        // Benchmark.Net

        var segments = new List<string>();
        segments.AddIfNotNull("utm_source", Campaign.Source);
        segments.AddIfNotNull("utm_medium", Campaign.Medium);
        segments.AddIfNotNull("utm_campaign", Campaign.Name);
        segments.AddIfNotNull("utm_id", Campaign.Id);
        segments.AddIfNotNull("utm_term", Campaign.Term);
        segments.AddIfNotNull("utm_content", Campaign.Content);

        return $"{Url.Address}?{string.Join("&", segments)}";
    }

    // Implicit Operator
    // Faz a conversão implícita diretamente ao fazer uma atribuição 
    // ex: string minhaString = utm (Utm é um objeto);
    public static implicit operator string(Utm utm) => utm.ToString();

    public static implicit operator Utm(string link)
    {
        if (string.IsNullOrEmpty(link))
        {
            throw new InvalidUrlException();
        }

        var url = new Url(link);
        var segments = url.Address.Split("?");

        if (segments.Length == 1)
        {
            throw new InvalidUrlException("No segments were provided");
        }

        var urlQueryParams = segments[1].Split("&");
        var map = new Dictionary<string, string>(6)
        {
            { "utm_source", string.Empty },
            { "utm_medium", string.Empty },
            { "utm_campaign", string.Empty },
            { "utm_id", string.Empty },
            { "utm_term", string.Empty },
            { "utm_content", string.Empty }
        };

        foreach (var queryParam in urlQueryParams)
        {
            var (Key, Value) = queryParam.QueryStringTuple();
            map[Key] = Value;
        }

        var utm = new Utm(
            new Url(segments[0]),
            new Campaign(map["utm_source"], map["utm_medium"], map["utm_campaign"], map["utm_id"], map["utm_term"], map["utm_content"]));

        return utm;
    }
}