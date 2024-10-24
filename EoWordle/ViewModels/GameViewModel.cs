using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using EoWordle.Models;
using EoWordle.Util;
using EoWordle.Views;

namespace EoWordle.ViewModels;

public class GameViewModel : INotifyPropertyChanged
{
    private readonly GameModel _gameModel;
    private UserControl _currentView;
    private bool _hasWon;
    private bool _isGameOver;
    private string _currentGuess;

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel(GameModel gameModel, GameView gameView)
    {
        _gameModel = gameModel;
        _currentView = gameView;
        _currentGuess = string.Empty;
    }

    // ensure that a word entered via the cmd is valid before setting it.
    public void SetUpCustomWord(string customWord)
    {
        string trimmedCustomWord = customWord.Trim().ToUpper();
        if (_gameModel.WordExistsInList(trimmedCustomWord) && GameConfig.WordLength == trimmedCustomWord.Length)
        {
            _gameModel.SetCorrectWord(trimmedCustomWord);
        }
    }

    public bool HasWon
    {
        get => _hasWon;
        private set
        {
            _hasWon = value;
            OnPropertyChanged(nameof(HasWon));
        }
    }

    public bool IsGameOver
    {
        get => _isGameOver;
        private set
        {
            _isGameOver = value;
            OnPropertyChanged(nameof(IsGameOver));
        }
    }

    public string CurrentGuess
    {
        get => _currentGuess;
        set
        {
            _currentGuess = value;
            OnPropertyChanged(nameof(CurrentGuess));
        }
    }

    public UserControl CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged(nameof(CurrentView));
        }
    }

    public void SubmitGuess()
    {
        _currentGuess = _currentGuess.Trim().ToUpper();
        if (_currentGuess.Length != GameConfig.WordLength)
        {
            MessageBox.Show($"Your guess needs to be {GameConfig.WordLength} letters long.");
            return;
        }

        if (!_gameModel.WordExistsInList(_currentGuess))
        {
            MessageBox.Show("Word does not exist in the word list. Please try again.");
            return;
        }

        var result = _gameModel.CheckGuess(_currentGuess);

        if (result != null)
        {
            var gameView = CurrentView as GameView;
            gameView?.AddGuessToGrid(result);

            if (_gameModel.WonGame(_currentGuess))
            {
                _hasWon = true;
                MessageBox.Show($"Nice job! The correct word was {_gameModel.GetCorrectWord()}.");
                ResetGame();
            }
            else if (_gameModel.GameOver())
            {
                _isGameOver = true;
                MessageBox.Show($"Game over! The correct word was {_gameModel.GetCorrectWord()}.");
                ResetGame();
            }
            else
            {
                ClearGuess();
            }
        }
    }

    public void ClearGuess()
    {
        CurrentGuess = string.Empty;
    }

    public void ResetGame()
    {
        _gameModel.ResetGame();
        _hasWon = false;
        _isGameOver = false;
        ClearGuess();

        var gameView = CurrentView as GameView;
        gameView?.ResetGrid();
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}