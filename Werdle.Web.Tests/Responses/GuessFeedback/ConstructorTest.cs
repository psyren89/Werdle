namespace Werdle.Tests.Responses.GuessFeedback;

using System;
using Shouldly;
using Web.Responses;
using Xunit;

public class ConstructorTest
{
    [Fact]
    public void WhenFeedbackIsNull_ThrowsArgumentNullException()
    {
        var game = new GameState(Guid.NewGuid(), 1, "abc");

        ((Action)(() => new GuessFeedback(game, null))).ShouldThrow<ArgumentNullException>();
    }
}
