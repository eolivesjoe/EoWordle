using System.IO;
using System.Reflection;

namespace EoWordle.Services;

public class WordService : IWordService
{
    private readonly List<string> _words = new();
    private const string _filePath = "EoWordle.Util.words.txt";

    public WordService()
    {
        _words = LoadWordsFromFile(_filePath);
    }

    public string GetWord()
    {
        var rand = new Random();
        return _words[rand.Next(0, _words.Count)];
    }

    private List<string> LoadWordsFromFile(string filePath)
    {
        var words = new List<string>();

        var assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream(filePath))
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), $"Resource '{filePath}' not found.");
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    words.Add(line.Trim());
                    line = reader.ReadLine();
                }
            }
        }

        return words;
    }

}

