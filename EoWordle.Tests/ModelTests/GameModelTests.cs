
using EoWordle.Models;
using EoWordle.Services;
using EoWordle.Util;
using NSubstitute;
using System.Windows.Media;

namespace EoWordle.Tests.ModelTests;

public class GameModelTests
{
    private GameModel _gameModel;
    private IGameService _gameService;
    private IWordService _wordService;

    [SetUp]
    public void SetUp()
    {
        _gameService = Substitute.For<IGameService>();
        _wordService = Substitute.For<IWordService>();

        _wordService.GetRandomWord().Returns("apple");
        _wordService.GetWordList().Returns(new List<string> { "apple", "grape", "melon" });

        _gameModel = new GameModel(_wordService, _gameService);
    }

    [Test]
    public void SetCorrectWord()
    {
        // Act
        _gameModel.SetCorrectWord("grape");

        // Assert
        Assert.That(_gameModel.GetCorrectWord(), Is.EqualTo("grape"));
    }

    [Test]
    public void CheckGuess()
    {
        // Arrange
        string guess = "apple";

        SolidColorBrush[] brushes = new SolidColorBrush[5];
        for (int i = 0; i < brushes.Length; i++)
        {
            brushes[i] = new SolidColorBrush(Colors.Gray);
        }

        var guessResult = new GuessResult(guess, brushes);
        _gameService.CheckGuess(guess, "apple").Returns(guessResult);

        // Act
        var result = _gameModel.CheckGuess(guess);

        // Assert
        Assert.That(result, Is.SameAs(guessResult));
        _gameService.Received(1).CheckGuess(guess, "apple");
    }

    [Test]
    public void WordExistsInList()
    {
        // Act
        var result = _gameModel.WordExistsInList("grape");

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void WordDoesNotExistsInList()
    {
        // Act
        var result = _gameModel.WordExistsInList("banana");

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void WonGameCorrectGuess()
    {
        // Assert
        _gameModel.SetCorrectWord("apple");

        // Act
        var result = _gameModel.WonGame("apple");

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void WonGameIncorrectGuess()
    {
        // Assert
        _gameModel.SetCorrectWord("apple");

        // Act
        var result = _gameModel.WonGame("banana");

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GameOverd()
    {
        // Arrange
        for (int i = 0; i < GameConfig.MaxGuesses; i++)
        {
            _gameModel.CheckGuess("guess");
        }

        // Act
        var result = _gameModel.GameOver();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void GamNoteOver()
    {
        // Arrange
        _gameModel.CheckGuess("guess");

        // Act
        var result = _gameModel.GameOver();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ResetGame()
    {
        // Arrange
        _gameModel.CheckGuess("guess");
        var newWord = "melon";
        _wordService.GetRandomWord().Returns(newWord);

        // Act
        _gameModel.ResetGame();

        // Assert
        Assert.That(_gameModel.GetCorrectWord(), Is.EqualTo(newWord));
        Assert.IsFalse(_gameModel.GameOver());
    }
}
