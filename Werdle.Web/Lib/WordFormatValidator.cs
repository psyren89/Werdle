namespace Werdle.Web.Lib;

using System.Text.RegularExpressions;
using Interfaces;

public class WordFormatValidator : IWordFormatValidator
{
    /// <summary>
    /// Validates the format of a word, not the word itself.
    /// </summary>
    /// <param name="word">The word to validate.</param>
    /// <returns>True iff <c>word</c> is exactly 5 (five) alphabetic characters.</returns>
    public bool IsValid(string word)
    {
        if (string.IsNullOrEmpty(word))
        {
            return false;
        }

        return Regex.IsMatch(word, "^[A-Z]{5}$", RegexOptions.IgnoreCase);
    }
}
