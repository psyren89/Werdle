namespace Werdle.Tests.Lib.WordFormatValidator;

using Shouldly;
using Web.Lib;
using Web.Lib.Interfaces;
using Xunit;

public class IsValidTest
{
    private readonly IWordFormatValidator validator = new WordFormatValidator();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenWordIsNotPresent_ReturnsFalse(string word) =>
        validator.IsValid(word).ShouldBeFalse();

    [Fact]
    public void WhenWordContainsNonAlphabeticCharacters_ReturnsFalse() => validator.IsValid("ab12!").ShouldBeFalse();

    [Fact]
    public void WhenWordIsTooShort_ReturnsFalse() => validator.IsValid("abcd").ShouldBeFalse();

    [Fact]
    public void WhenWordIsTooLong_ReturnsFalse() => validator.IsValid("abcdef").ShouldBeFalse();

    [Fact]
    public void WhenWordIsOnlyAlphabeticCharactersAndCorrectLength_ReturnsTrue() =>
        validator.IsValid("abcde").ShouldBeTrue();
}
