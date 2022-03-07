namespace Werdle.Tests.Lib.CsvWordSource;

using System;
using Moq;
using Shouldly;
using Web.Lib;
using Web.Lib.Interfaces;
using Xunit;

public class ConstructorTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenCsvPathIsNotPresent_ThrowsArgumentException(string csvPath) =>
        ((Action)(() => new CsvWordSource(Mock.Of<ICsvWordFileReader>(), csvPath))).ShouldThrow<ArgumentException>();
}
