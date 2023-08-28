using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Tests.ValueObjects;


[TestClass]
public class UrlTests
{
    private const string InvalidUrl = "banana";
    private const string ValidUrl = "https://balta.io";

    [TestMethod("Deve retornar uma exceção quando a url for inválida")] // exemplo de mensagem no dataAnottation (é opcional) 
    [TestCategory("Teste de URL")] // exemplo de categorização do teste (é opcional)
    [ExpectedException(typeof(InvalidUrlException))]
    public void ShouldReturnExceptionWhenUrlIsInvalid()
    {
        new Url(InvalidUrl);
    }

    [TestMethod]
    public void ShouldNotReturnExceptionWhenUrlIsValid()
    {
        var url = new Url(ValidUrl);
        Assert.IsTrue(true);
    }


    // Uma outra abordagem que para este caso não faz muito sentido, mas fica como exemplo quando for necessário testar vários valores diferentes
    [TestMethod]
    [DataRow(" ", true)]
    [DataRow("http", true)]
    [DataRow("banana", true)]
    [DataRow("https://balta.io", false)]
    public void ShouldNotReturnExceptionWhenUrlIsValid2(string link, bool expectException)
    {
        if (expectException)
        {
            try
            {
                new Url(link);
                Assert.IsTrue(true);
            }
            catch (InvalidUrlException)
            {
                Assert.IsTrue(true);
            }
        }
        else
        {
            var url = new Url(link);
            Assert.IsTrue(true);
        }
    }
}