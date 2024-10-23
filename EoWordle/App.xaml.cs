using EoWordle.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EoWordle;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    // Set up DI for the project
    public App()
    {
        ServiceCollection serviceColletion = new();
        serviceColletion.ConfigureServices();

        _serviceProvider = serviceColletion.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        string customWord = string.Empty;
        if(e.Args.Length > 0 && !string.IsNullOrEmpty(e.Args[0]))
        {
            customWord = e.Args[0].Trim().ToUpper();
            var gameViewModel = _serviceProvider?.GetRequiredService<GameViewModel>();
            gameViewModel?.SetUpCustomWord(customWord);
        }

        var mainWindow = _serviceProvider?.GetRequiredService<MainWindow>();
        mainWindow?.Show();
    }
}