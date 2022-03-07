namespace Werdle.Web.Lib;

using Interfaces;

public class CsvWordSourceFactory : ICsvWordSourceFactory
{
    private readonly ICsvWordFileReader csvWordFileReader;

    public CsvWordSourceFactory(ICsvWordFileReader csvWordFileReader) => this.csvWordFileReader = csvWordFileReader;

    public IWordSource Create() => new CsvWordSource(csvWordFileReader);

    public IWordSource Create(string csvPath) => new CsvWordSource(csvWordFileReader, csvPath);
}
