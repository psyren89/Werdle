namespace Werdle.Tests.Responses.GameState;

using System;
using Shouldly;
using Web.Responses;
using Web.Responses.Enums;
using Xunit;

public class EndGameTest
{
    private readonly GameState game = new(Guid.NewGuid(), 10, "abc");

    [Theory]
    [InlineData(GameOutcome.None)]
    [InlineData(GameOutcome.InProgress)]
    public void WhenOutcomeIsNotWinOrLoss_ThrowsArgumentOutOfRangeException(GameOutcome outcome) =>
        ((Action)(() => game.EndGame(outcome))).ShouldThrow<ArgumentOutOfRangeException>();

    [Fact]
    public void SetsRemainingGuessesToZero()
    {
        game.EndGame(GameOutcome.Win);

        game.RemainingGuesses.ShouldBe(0);
    }

    [Theory]
    [InlineData(GameOutcome.Win)]
    [InlineData(GameOutcome.Loss)]
    public void SetsGameOutcomeToOutcome(GameOutcome outcome)
    {
        game.EndGame(outcome);

        game.Outcome.ShouldBe(outcome);
    }
}
