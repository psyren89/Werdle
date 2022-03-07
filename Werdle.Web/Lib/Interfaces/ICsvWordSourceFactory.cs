namespace Werdle.Web.Lib.Interfaces;

public interface ICsvWordSourceFactory
{
    IWordSource Create();

    IWordSource Create(string csvPath);
}
