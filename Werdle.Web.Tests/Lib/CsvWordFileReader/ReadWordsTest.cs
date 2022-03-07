namespace Werdle.Tests.Lib.CsvWordFileReader;

using System;
using System.IO;
using System.Linq;
using Moq;
using Shouldly;
using Support;
using Web.Lib;
using Web.Lib.Interfaces;
using Xunit;

public class ReadWordsTest
{
    private readonly IWordFormatValidator validator = Mock.Of<IWordFormatValidator>();
    private readonly ICsvWordFileReader reader;

    public ReadWordsTest() => reader = new CsvWordFileReader(validator);

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenCsvPathIsNotPresent_ThrowsArgumentException(string csvPath) =>
        ((Action)(() => reader.ReadWords(csvPath).ToList())).ShouldThrow<ArgumentException>();

    [Fact]
    public void CallsCsvWordFormatValidatorWithEachWord()
    {
        Mock.Get(validator).Setup(m => m.IsValid(It.IsAny<string>())).Returns(true);

        reader.ReadWords(Paths.CsvFile).ToList();

        Mock.Get(validator).Verify(m => m.IsValid("apple"));
        Mock.Get(validator).Verify(m => m.IsValid("peach"));
    }

    [Fact]
    public void WhenValidatorReturnsFalse_ThrowsException()
    {
        Mock.Get(validator).Setup(m => m.IsValid(It.IsAny<string>())).Returns(false);

        ((Action)(() => reader.ReadWords(Paths.CsvFile).ToList())).ShouldThrow<Exception>();
    }

    [Fact]
    public void WhenValidatorReturnsTrue_ReturnsWord()
    {
        Mock.Get(validator).Setup(m => m.IsValid(It.IsAny<string>())).Returns(true);

        reader.ReadWords(Paths.CsvFile).ShouldContain("apple");
    }
}
