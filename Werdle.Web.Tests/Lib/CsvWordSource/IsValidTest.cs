namespace Werdle.Tests.Lib.CsvWordSource;

using Moq;
using Shouldly;
using Web.Lib;
using Web.Lib.Interfaces;
using Xunit;

public class IsValidTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenWordIsNotPresent_ReturnsFalse(string word)
    {
        var source = new CsvWordSource(Mock.Of<ICsvWordFileReader>());

        source.IsValid(word).ShouldBeFalse();
    }

    [Fact]
    public void WhenWordIsNotInCsv_ReturnsFalse()
    {
        var source = new CsvWordSource(Mock.Of<ICsvWordFileReader>());

        source.IsValid("test").ShouldBeFalse();
    }

    [Fact]
    public void WhenWordIsInCsv_ReturnsTrue()
    {
        var reader = Mock.Of<ICsvWordFileReader>();
        Mock.Get(reader).Setup(m => m.ReadWords(It.IsAny<string>())).Returns(new[] { "apple" });

        var source = new CsvWordSource(reader);

        source.IsValid("apple").ShouldBeTrue();
    }
}
