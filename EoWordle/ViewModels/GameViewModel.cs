using System.ComponentModel;
using EoWordle.Services;
using System.Windows.Controls;
using EoWordle.Views;

namespace EoWordle.ViewModels;

public class GameViewModel : INotifyPropertyChanged
{
    private UserControl _currentView;
    private readonly IWordService _wordService;
    private string _randomWord;

    public event PropertyChangedEventHandler? PropertyChanged;

    public GameViewModel()
    {
        _wordService = new WordService();
        CurrentView = new GameView();
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
