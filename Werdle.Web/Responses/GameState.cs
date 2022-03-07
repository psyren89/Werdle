namespace Werdle.Web.Responses;

using System.Text.Json.Serialization;
using Enums;

public class GameState
{
    [JsonConstructor]
    public GameState()
    {
    }

    public GameState(Guid id, int maxGuesses, string word)
    {
        if (id == default || id == Guid.Empty)
        {
            throw new ArgumentException("Invalid GUID", nameof(id));
        }

        if (maxGuesses < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(maxGuesses), maxGuesses, "Must provide a positive number");
        }

        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentException("Must provide a word", nameof(word));
        }

        Id = id;
        Outcome = GameOutcome.InProgress;
        RemainingGuesses = maxGuesses;
        Word = word;
    }

    public Guid Id { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GameOutcome Outcome { get; set; }

    public int RemainingGuesses { get; set; }

    [JsonIgnore] // Don't send the answer along with game state.
    public string? Word { get; }

    public void EndGame(GameOutcome outcome)
    {
        if (outcome is not (GameOutcome.Win or GameOutcome.Loss))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Game must end in win or loss");
        }

        Outcome = outcome;
        RemainingGuesses = 0;
    }

    public void UseGuess()
    {
        if (RemainingGuesses < 1)
        {
            throw new Exception("No guesses remain");
        }

        RemainingGuesses--;
    }
}
