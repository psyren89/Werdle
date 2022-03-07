namespace Werdle.Web.Lib;

using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Interfaces;

public class CsvWordFileReader : ICsvWordFileReader
{
    // For now, assume all files will look like the provided one.
    private readonly CsvConfiguration configuration = new(CultureInfo.InvariantCulture) { HasHeaderRecord = false };
    private readonly IWordFormatValidator wordFormatValidator;

    public CsvWordFileReader(IWordFormatValidator wordFormatValidator) =>
        this.wordFormatValidator = wordFormatValidator;

    public IEnumerable<string> ReadWords(string csvPath)
    {
        if (string.IsNullOrEmpty(csvPath))
        {
            throw new ArgumentException("Must provide path to CSV", nameof(csvPath));
        }

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, configuration);

        var lineNumber = 1;
        while (csv.Read())
        {
            var word = csv.GetField<string>(0);
            if (!wordFormatValidator.IsValid(word))
            {
                throw new Exception($"Invalid/missing word found on line {lineNumber}");
            }

            yield return word;

            lineNumber++;
        }
    }
}
