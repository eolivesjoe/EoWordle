using System.ComponentModel;
using EoWordle.Services;
using System.Windows.Controls;
using EoWordle.Views;

namespace EoWordle.ViewModels;

public class GameViewModel : INotifyPropertyChanged
{
    private UserControl _currentView;
    private readonly IWordService _wordService;
    private readonly IGameService _gameService;
    private string _randomWord;

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel(IWordService wordService, IGameService gameService, GameView gameView)
    {
        _wordService = wordService;
        _gameService = gameService;

        CurrentView = gameView;
        RandomWord = _wordService.GetWord();
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

    public string RandomWord
    {
        get => _randomWord;
        set
        {
            _randomWord = value;
            OnPropertyChanged(nameof(RandomWord));
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
