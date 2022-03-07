namespace Werdle.Tests.Responses.GameOver;

using System;
using System.Linq;
using Shouldly;
using Web.Responses;
using Web.Responses.Enums;
using Xunit;

public class ConstructorTest
{
    [Fact]
    public void WhenIdIsDefault_ThrowsArgumentException() =>
        ((Action)(() => new GameOver(default, "abc", GameOutcome.Win, Enumerable.Empty<LetterFeedback>())))
        .ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenIdIsEmpty_ThrowsArgumentException() =>
        ((Action)(() => new GameOver(Guid.Empty, "abc", GameOutcome.Win, Enumerable.Empty<LetterFeedback>())))
        .ShouldThrow<ArgumentException>();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenWordIsNotPresent_ThrowsArgumentException(string word) =>
        ((Action)(() => new GameOver(Guid.NewGuid(), word, GameOutcome.Win, Enumerable.Empty<LetterFeedback>())))
        .ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenOutcomeIsNone_ThrowsArgumentOutOfRangeException() =>
        ((Action)(() => new GameOver(Guid.NewGuid(), "abc", GameOutcome.None, Enumerable.Empty<LetterFeedback>())))
        .ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenFeedbackIsNull_ThrowsArgumentNullException() =>
        ((Action)(() => new GameOver(Guid.NewGuid(), "abc", GameOutcome.Win, null))).ShouldThrow<ArgumentException>();
}
