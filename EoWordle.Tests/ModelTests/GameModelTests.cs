
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

        _wordService.GetRandomWord().Returns("APPLE");
        _wordService.GetWordList().Returns(new List<string> { "APPLE", "GRAPE", "MELON" });

        _gameModel = new GameModel(_wordService, _gameService);
    }

    [Test]
    public void SetCorrectWord()
    {
        // Act
        _gameModel.SetCorrectWord("GRAPE");

        // Assert
        Assert.That(_gameModel.GetCorrectWord(), Is.EqualTo("GRAPE"));
    }

    [Test]
    public void CheckGuess()
    {
        // Arrange
        string guess = "APPLE";

        SolidColorBrush[] brushes = new SolidColorBrush[5];
        for (int i = 0; i < brushes.Length; i++)
        {
            brushes[i] = new SolidColorBrush(Colors.Gray);
        }

        var guessResult = new GuessResult(guess, brushes);
        _gameService.CheckGuess(guess, "APPLE").Returns(guessResult);

        // Act
        var result = _gameModel.CheckGuess(guess);

        // Assert
        Assert.That(result, Is.SameAs(guessResult));
        _gameService.Received(1).CheckGuess(guess, "APPLE");
    }

    [Test]
    public void WordExistsInList()
    {
        // Act
        var result = _gameModel.WordExistsInList("GRAPE");

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void WordDoesNotExistsInList()
    {
        // Act
        var result = _gameModel.WordExistsInList("BANANA");

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void WonGameCorrectGuess()
    {
        // Assert
        _gameModel.SetCorrectWord("APPLE");

        // Act
        var result = _gameModel.WonGame("APPLE");

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void WonGameIncorrectGuess()
    {
        // Assert
        _gameModel.SetCorrectWord("APPLE");

        // Act
        var result = _gameModel.WonGame("BANANA");

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void GameOver()
    {
        // Arrange
        for (int i = 0; i < GameConfig.MaxGuesses; i++)
        {
            _gameModel.CheckGuess("APPLE");
        }

        // Act
        var result = _gameModel.GameOver();

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void GameNotOver()
    {
        // Arrange
        _gameModel.CheckGuess("APPLE");

        // Act
        var result = _gameModel.GameOver();

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void ResetGame()
    {
        // Arrange
        _gameModel.CheckGuess("APPLE");
        var newWord = "MELON";
        _wordService.GetRandomWord().Returns(newWord);

        // Act
        _gameModel.ResetGame();

        // Assert
        Assert.That(_gameModel.GetCorrectWord(), Is.EqualTo(newWord));
        Assert.IsFalse(_gameModel.GameOver());
    }
}
