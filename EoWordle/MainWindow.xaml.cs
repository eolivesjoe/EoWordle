using EoWordle.ViewModels;
using System.Windows;

namespace EoWordle;

public partial class MainWindow : Window
{
    public MainWindow(GameViewModel gameViewModel)
    {
        InitializeComponent();
        DataContext = gameViewModel;
    }
}
