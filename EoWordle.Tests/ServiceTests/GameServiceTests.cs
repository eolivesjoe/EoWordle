using EoWordle.Models;
using EoWordle.Services;
using System.Windows.Media;

namespace EoWordle.Tests.ServiceTests;

public class GameServiceTests
{
    private readonly IGameService _gameService;

    public GameServiceTests()
    {
        _gameService = new GameService();
    }

    [Test]
    public void CheckGuess_CorrectGuess()
    {
        // Arrange
        string guess = "APPLE";
        string correctWord = "APPLE";

        // Act
        GuessResult result = _gameService.CheckGuess(guess, correctWord);

        // Assert
        foreach (var colour in result.Colours)
        {
            Assert.That(colour, Is.EqualTo(Brushes.Green));
        }
    }

    [Test]
    public void CheckGuess_PartialMatch_GrayAndYellow()
    {
        // Arrange
        string guess = "PIZZA";
        string correctWord = "APPLE";

        // Act
        GuessResult result = _gameService.CheckGuess(guess, correctWord);

        // Assert
        Assert.That(result.Colours[0], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[1], Is.EqualTo(Brushes.Gray));
        Assert.That(result.Colours[2], Is.EqualTo(Brushes.Gray));
        Assert.That(result.Colours[3], Is.EqualTo(Brushes.Gray));
        Assert.That(result.Colours[4], Is.EqualTo(Brushes.Yellow));
    }

    [Test]
    public void CheckGuess_PartialMatch_AllYellow()
    {
        // Arrange
        string guess = "LEAPP";
        string correctWord = "APPLE";

        // Act
        GuessResult result = _gameService.CheckGuess(guess, correctWord);

        // Assert
        Assert.That(result.Colours[0], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[1], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[2], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[3], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[4], Is.EqualTo(Brushes.Yellow));
    }

    [Test]
    public void CheckGuess_NoMatches()
    {
        // Arrange
        string guess = "ZZZZZ";
        string correctWord = "WATER";

        // Act
        GuessResult result = _gameService.CheckGuess(guess, correctWord);

        // Assert
        foreach (var colour in result.Colours)
        {
            Assert.That(colour, Is.EqualTo(Brushes.Gray));
        }
    }

    [Test]
    public void CheckGuess_MultipleChars()
    {
        // Arrange
        string guess = "PAPPE";
        string correctWord = "APPLE";

        // Act
        GuessResult result = _gameService.CheckGuess(guess, correctWord);

        // Assert
        Assert.That(result.Colours[0], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[1], Is.EqualTo(Brushes.Yellow));
        Assert.That(result.Colours[2], Is.EqualTo(Brushes.Green));
        Assert.That(result.Colours[3], Is.EqualTo(Brushes.Gray));
        Assert.That(result.Colours[4], Is.EqualTo(Brushes.Green));
    }
}