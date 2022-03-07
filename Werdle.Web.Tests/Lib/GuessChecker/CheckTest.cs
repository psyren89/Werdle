namespace Werdle.Tests.Lib.GuessChecker;

using System;
using Shouldly;
using Web.Lib;
using Web.Lib.Interfaces;
using Web.Responses.Enums;
using Xunit;

public class CheckTest
{
    private readonly IGuessChecker checker = new GuessChecker();

    [Fact]
    public void WhenGameWordAndGuessWordDoNotHaveEqualLength_ThrowsArgumentException() =>
        ((Action)(() => checker.Check("abc", "12345"))).ShouldThrow<ArgumentException>();

    [Fact]
    public void WhenGameWordContainsGuessWordLetterInCorrectPosition_ReturnsLetterFeedback()
    {
        var letterFeedbacks = checker.Check("a", "a");
        letterFeedbacks.ShouldContain(lf => lf.Letter == 'A' && lf.Validity == LetterValidity.Correct);
    }

    [Fact]
    public void WhenGameWordDoesNotContainGuessWordLetter_ReturnsLetterFeedback()
    {
        var letterFeedbacks = checker.Check("a", "b");
        letterFeedbacks.ShouldContain(lf => lf.Letter == 'B' && lf.Validity == LetterValidity.NotPresent);
    }

    [Fact]
    public void WhenGameWordContainsGuessWordLetterInWrongPositionWithNoOtherOccurrences_ReturnsLetterFeedback()
    {
        var letterFeedbacks = checker.Check("ab", "bc");
        letterFeedbacks.ShouldContain(lf => lf.Letter == 'B' && lf.Validity == LetterValidity.WrongPosition);
    }

    [Fact]
    public void WhenGameWordContainsGuessWordLetterInWrongPositionWithOtherOccurrences_ReturnsLetterFeedback()
    {
        var letterFeedbacks = checker.Check("ab", "bb");
        letterFeedbacks.ShouldContain(lf => lf.Letter == 'B' && lf.Validity == LetterValidity.NotPresent);
    }
}
