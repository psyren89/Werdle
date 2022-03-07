namespace Werdle.Web.Responses;

using System.Text.Json.Serialization;
using Enums;

public class GameOver
{
    public GameOver(Guid id, string word, GameOutcome outcome, IEnumerable<LetterFeedback> feedback)
    {
        if (id == default || id == Guid.Empty)
        {
            throw new ArgumentException("Invalid GUID", nameof(id));
        }

        if (string.IsNullOrEmpty(word))
        {
            throw new ArgumentException("Must provide a word", nameof(word));
        }

        if (outcome == GameOutcome.None)
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Invalid game outcome");
        }

        Id = id;
        Word = word;
        Outcome = outcome;
        Feedback = feedback ?? throw new ArgumentNullException(nameof(feedback), "Must pass feedback");
    }

    public IEnumerable<LetterFeedback> Feedback { get; }

    public Guid Id { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GameOutcome Outcome { get; }

    public string Word { get; }
}
