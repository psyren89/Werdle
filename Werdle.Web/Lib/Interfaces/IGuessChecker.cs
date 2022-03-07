namespace Werdle.Web.Lib.Interfaces;

using Responses;

public interface IGuessChecker
{
    IEnumerable<LetterFeedback> Check(string gameWord, string guessWord);
}
