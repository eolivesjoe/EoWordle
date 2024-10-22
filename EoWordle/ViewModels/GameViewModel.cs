using System.ComponentModel;
using System.Windows.Controls;
using EoWordle.Views;
using EoWordle.Models;

namespace EoWordle.ViewModels;

public class GameViewModel : INotifyPropertyChanged
{
    private UserControl _currentView;
    private readonly GameModel _gameModel;

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel(GameView gameView, GameModel gameModel)
    {
        CurrentView = gameView;
        _gameModel = gameModel;
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public GuessResult SubmitGuess(string guess)
    {
        return _gameModel.CheckGuess(guess);
    }
}
