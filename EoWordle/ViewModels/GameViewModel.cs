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
    private string _correctWord;
    private bool _hasWon;
    private bool _isGameOver;
    private string _currentGuess;

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel(GameModel gameModel, GameView gameView)
    {
        _gameModel = gameModel;
        _currentView = gameView;
        _correctWord = _gameModel.GetCorrectWord();
        _currentGuess = string.Empty;
    }

    public string CorrectWord
    {
        get => _correctWord;
        private set
        {
            _correctWord = value;
            OnPropertyChanged(nameof(CorrectWord));
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

    public void SubmitCurrentGuess()
    {
        _currentGuess = _currentGuess.Trim().ToUpper();
        if (_currentGuess.Length != GameConfig.WordLength)
        {
            MessageBox.Show("Please enter the correct length of the guess.");
            return;
        }

        if (!_gameModel.DoesWordExistInList(_currentGuess))
        {
            MessageBox.Show("Word does not exist in the word list. Try again.");
            return;
        }

        var result = _gameModel.CheckGuess(_currentGuess);

        if (result != null)
        {
            var gameView = CurrentView as GameView;
            gameView.AddGuessToGrid(result);

            if (_gameModel.WonGame(_currentGuess))
            {
                _hasWon = true;
                MessageBox.Show($"Nice job! The correct word was {_correctWord}.");
                ResetGame();
            }
            else if (_gameModel.GameOver())
            {
                _isGameOver = true;
                MessageBox.Show($"Game over! The correct word was {_correctWord}.");
                ResetGame();
            }
            else
            {
                ClearCurrentGuess();
            }
        }
    }

    public void ClearCurrentGuess()
    {
        CurrentGuess = string.Empty;
    }

    public void ResetGame()
    {
        _gameModel.ResetGame();
        _correctWord = _gameModel.GetCorrectWord();
        _hasWon = false;
        _isGameOver = false;
        ClearCurrentGuess();

        var gameView = CurrentView as GameView;
        gameView.ResetGrid();
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
