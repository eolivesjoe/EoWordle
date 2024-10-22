using System.IO;
using System.Reflection;

namespace EoWordle.Services;

// Load in words from the txt file and get a random target word when starting the game
public class WordService : IWordService
{
    private readonly List<string> _words = new();
    private const string _filePath = "EoWordle.Util.words.txt";

    public WordService()
    {
        LoadWordsFromFile(_filePath);
    }
    public string GetWord()
    {
        var rand = new Random();
        return _words[rand.Next(0, _words.Count)];
    }

    private void LoadWordsFromFile(string filePath)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream stream = assembly.GetManifestResourceStream(filePath))
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), $"Resource not found.");
            }

            using (StreamReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    _words.Add(line.Trim().ToUpper());
                    line = reader.ReadLine();
                }
            }
        }
    }
}

