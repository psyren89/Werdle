namespace Werdle.Tests.Responses.GameState;

using System;
using Shouldly;
using Web.Responses;
using Xunit;

public class ConstructorTest
{
    [Fact]
    public void WhenIdIsDefault_ThrowsArgumentException() =>
        ((Action)(() => new GameState(default, 1, "abc"))).ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenIdIsEmpty_ThrowsArgumentException() =>
        ((Action)(() => new GameState(Guid.Empty, 1, "abc"))).ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenMaxGuessesIsLessThanOne_ThrowsArgumentOutOfRangeException() =>
        ((Action)(() => new GameState(Guid.NewGuid(), 0, "abc"))).ShouldThrow<ArgumentOutOfRangeException>();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenWordIsNotPresent_ThrowsArgumentException(string word) =>
        ((Action)(() => new GameState(Guid.NewGuid(), 1, word))).ShouldThrow<ArgumentException>();
}
