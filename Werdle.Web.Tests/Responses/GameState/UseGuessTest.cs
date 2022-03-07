namespace Werdle.Tests.Responses.GameState;

using System;
using Shouldly;
using Web.Responses;
using Xunit;

public class UseGuessTest
{
    [Fact]
    public void WhenRemainingGuessesIsGreaterThanZero_DecrementsRemainingGuesses()
    {
        var game = new GameState(Guid.NewGuid(), 1, "abc");

        game.UseGuess();

        game.RemainingGuesses.ShouldBe(0);
    }

    [Fact]
    public void WhenRemainingGuessesLessThanOne_ThrowsException()
    {
        var game = new GameState(Guid.NewGuid(), 1, "abc");

        game.UseGuess();

        ((Action)(() => game.UseGuess())).ShouldThrow<Exception>();
    }
}
