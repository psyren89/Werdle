namespace Werdle.Web.Responses;

public class GuessFeedback : GameState
{
    public GuessFeedback(GameState gameState, IEnumerable<LetterFeedback> feedback) :
        base(gameState.Id, gameState.RemainingGuesses, gameState.Word) =>
        Feedback = feedback ?? throw new ArgumentNullException(nameof(feedback), "Must pass feedback");

    public IEnumerable<LetterFeedback> Feedback { get; }
}
