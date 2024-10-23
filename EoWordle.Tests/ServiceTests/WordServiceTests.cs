using EoWordle.Services;

namespace EoWordle.Tests.ServiceTests;

public class WordServiceTests
{
    private IWordService _wordService;

    [SetUp]
    public void Setup()
    {
        _wordService = new WordService();
    }

    [Test]
    public void GetWordList()
    {
        // Act
        var words = _wordService.GetWordList();

        // Assert
        Assert.IsNotEmpty(words);
        Assert.Contains("APPLE", words);
    }

    [Test]
    public void GetRandomWord()
    {
        // Arrange
        _wordService.GetRandomWord();

        // Act
        var randomWord = _wordService.GetRandomWord();

        // Assert
        Assert.IsNotNull(randomWord);
    }
}