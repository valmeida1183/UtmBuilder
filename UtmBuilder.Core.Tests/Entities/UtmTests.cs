using UtmBuilder.Core.Entities;
using UtmBuilder.Core.ValueObjects;

namespace UtmBuilder.Core.Tests.Entities;

[TestClass]
public class UtmTests
{
    private const string result = "https://balta.io/" +
            "?utm_source=src" +
            "&utm_medium=med" +
            "&utm_campaign=nme" +
            "&utm_id=id" +
            "&utm_term=ter" +
            "&utm_content=ctn";
    private readonly Url url = new("https://balta.io/");
    private readonly Campaign campaign = new("src", "med", "nme", "id", "ter", "ctn");

    [TestMethod]
    public void ShouldReturnUrlFromUtm()
    {
        var utm = new Utm(url, campaign);

        Assert.AreEqual(result, utm.ToString());
        Assert.AreEqual(result, (string)utm); // testa o implicit operator
    }

    [TestMethod]
    public void ShouldReturnUtmFromUrl()
    {
        Utm utm = result;

        Assert.AreEqual("https://balta.io/", utm.Url.Address);
        Assert.AreEqual("src", utm.Campaign.Source);
        Assert.AreEqual("med", utm.Campaign.Medium);
        Assert.AreEqual("nme", utm.Campaign.Name);
        Assert.AreEqual("id", utm.Campaign.Id);
        Assert.AreEqual("ter", utm.Campaign.Term);
        Assert.AreEqual("ctn", utm.Campaign.Content);
    }
}