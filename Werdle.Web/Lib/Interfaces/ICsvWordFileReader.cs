namespace Werdle.Web.Lib.Interfaces;

public interface ICsvWordFileReader
{
    IEnumerable<string> ReadWords(string csvPath);
}
