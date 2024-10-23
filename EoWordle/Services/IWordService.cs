namespace EoWordle.Services;

public interface IWordService
{
    public string GetRandomWord();
    public List<string> GetWordList();
}