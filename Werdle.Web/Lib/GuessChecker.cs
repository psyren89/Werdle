namespace Werdle.Web.Lib;

using Interfaces;
using Responses;
using Responses.Enums;

public class GuessChecker : IGuessChecker
{
    public IEnumerable<LetterFeedback> Check(string gameWord, string guessWord)
    {
        if (gameWord.Length != guessWord.Length)
        {
            throw new ArgumentException("Word lengths do not match");
        }

        var normalisedGameWord = gameWord.ToUpperInvariant();
        var normalisedGuessWord = guessWord.ToUpperInvariant();

        var letterFeedback =
            normalisedGuessWord.Select(guessLetter => new LetterFeedback { Letter = guessLetter }).ToList();

        for (var i = 0; i < normalisedGuessWord.Length; i++)
        {
            var guessLetter = normalisedGuessWord[i];

            LetterValidity validity;
            if (guessLetter == normalisedGameWord[i]) // Right letter, right position.
            {
                validity = LetterValidity.Correct;
            }
            else
            {
                validity = normalisedGameWord.Contains(guessLetter)
                    ? LetterValidity.WrongPosition
                    : LetterValidity.NotPresent;
            }

            letterFeedback[i].Validity = validity;
        }

        var gameWordLetterCounts = normalisedGameWord.GroupBy(l => l)
            .Select(g => new { Letter = g.Key, Count = g.Count() })
            .ToDictionary(gc => gc.Letter, gc => gc.Count);

        var correctLetterCounts = letterFeedback.Where(lf => lf.Validity == LetterValidity.Correct)
            .GroupBy(lf => lf.Letter)
            .Select(g => new { Letter = g.Key, Count = g.Count() })
            .ToDictionary(gc => gc.Letter, gc => gc.Count);

        var lettersInWrongPosition = letterFeedback.Where(lf => lf.Validity == LetterValidity.WrongPosition);
        foreach (var feedback in lettersInWrongPosition)
        {
            // If all instances of this letter have already been guessed correctly, return NotPresent.
            if (correctLetterCounts.TryGetValue(feedback.Letter, out var correctCount) &&
                correctCount == gameWordLetterCounts[feedback.Letter])
            {
                feedback.Validity = LetterValidity.NotPresent;
            }
        }

        return letterFeedback;
    }
}
