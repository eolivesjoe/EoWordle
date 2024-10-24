using EoWordle.Util;
using System.IO;
using System.Reflection;

namespace EoWordle.Services;

// Load in words from the txt file and get a random target word when starting the game
public class WordService : IWordService
{
    private readonly List<string> _words = new();
    private const string _filePath = "Util/words.txt";

    public WordService()
    {
        LoadWordsFromFile(_filePath);
    }

    public string GetRandomWord()
    {
        var rand = new Random();
        return _words[rand.Next(0, _words.Count)];
    }

    public List<string> GetWordList()
    {
        return _words;
    }

    private void LoadWordsFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            foreach (string line in File.ReadLines(filePath))
            {
                string trimmedWord = line.Trim();
                if (trimmedWord.Length == GameConfig.WordLength)
                {
                    _words.Add(trimmedWord.ToUpper());
                }
            }
        }
        else
        {
            Console.WriteLine("Please ensure that your words.txt exists in the Util folder.");
        }
    }
}