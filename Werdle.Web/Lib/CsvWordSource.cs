namespace Werdle.Web.Lib;

using Interfaces;

public class CsvWordSource : IWordSource
{
    private const string CsvFileName = "wordList.csv";

    private readonly ISet<string> validWords = new HashSet<string>();

    public CsvWordSource(ICsvWordFileReader csvWordFileReader, string csvPath)
    {
        if (string.IsNullOrEmpty(csvPath))
        {
            throw new ArgumentException("Must provide path to CSV", nameof(csvPath));
        }

        foreach (var word in csvWordFileReader.ReadWords(csvPath))
        {
            validWords.Add(word);
        }
    }

    public CsvWordSource(ICsvWordFileReader csvWordFileReader) : this(
        csvWordFileReader,
        Path.Join(AppDomain.CurrentDomain.BaseDirectory, CsvFileName))
    {
    }

    public bool IsValid(string word) => !string.IsNullOrEmpty(word) && validWords.Contains(word);
}
