using EoWordle.Models;
using EoWordle.Services;
using EoWordle.ViewModels;
using EoWordle.Views;
using NSubstitute;
using System.Windows;
using System.Windows.Media;

namespace EoWordle.Tests.ViewModelTests;

[TestFixture]
[Apartment(ApartmentState.STA)]
public class GameViewModelTests
{
    private GameModel _gameModel;
    private GameView _gameView;
    private GameViewModel _viewModel;

    private IWordService _wordService;
    private IGameService _gameService;


    [SetUp]
    public void Setup()
    {
        _gameView = new GameView();
        _gameService = Substitute.For<IGameService>();
        _wordService = Substitute.For<IWordService>();

        _gameModel = Substitute.ForPartsOf<GameModel>(_wordService, _gameService);
        _viewModel = new GameViewModel(_gameModel, _gameView);
    }

    [Test]
    public void SetUpCustomWordExists()
    {
        // Arrange
        string customWord = "HELLO";
        _wordService.GetWordList().Returns(new List<string> { "HELLO", "APPLE" });

        // Act
        _viewModel.SetUpCustomWord(customWord);

        // Assert
        _gameModel.Received().SetCorrectWord(customWord);

    }

    [Test]
    public void SetUpCustomWordDoesNotExists()
    {
        // Arrange
        string customWord = "ZZZZZ";
        _wordService.GetWordList().Returns(new List<string> { "HELLO", "APPLE" });

        // Act
        _viewModel.SetUpCustomWord(customWord);

        // Assert
        _gameModel.DidNotReceive().SetCorrectWord(customWord);
    }

    [Test]
    public void SubmitGuessWhenGuessLengthIsIncorrect()
    {
        // Arrange
        _viewModel.CurrentGuess = "BOB";

        // Act
        _viewModel.SubmitGuess();

        // Assert
        _gameModel.DidNotReceive().CheckGuess("BOB");

    }

    [Test]
    public void SubmitGuessWhenWordDoesNotExist()
    {
        // Arrange
        _viewModel.CurrentGuess = "HELLO";
        _wordService.GetWordList().Returns(new List<string> { "MELON", "APPLE" });

        // Act
        _viewModel.SubmitGuess();

        // Assert
        _gameModel.DidNotReceive().CheckGuess("HELLO");

    }

    [Test]
    public void ResetGameShouldResetGamePropertiesAndClearGuess()
    {
        // Arrange
        _viewModel.CurrentGuess = "HELLO";
        _gameModel.SetCorrectWord("HELLO");

        // Act
        _viewModel.ResetGame();

        // Assert
        Assert.IsFalse(_viewModel.HasWon);
        Assert.IsFalse(_viewModel.IsGameOver);
        Assert.IsEmpty(_viewModel.CurrentGuess);
    }
}