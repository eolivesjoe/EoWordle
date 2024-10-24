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

        GlobalExceptionHandlerRegistration();

        // set up custom word if entered
        string customWord = string.Empty;
        if(e.Args.Length > 0 && !string.IsNullOrEmpty(e.Args[0]))
        {
            customWord = e.Args[0];
            var gameViewModel = _serviceProvider?.GetRequiredService<GameViewModel>();
            gameViewModel?.SetUpCustomWord(customWord);
        }

        var mainWindow = _serviceProvider?.GetRequiredService<MainWindow>();
        mainWindow?.Show();
    }

    private void GlobalExceptionHandlerRegistration()
    {
        // catches exceptions thrown on the UI thread.
        DispatcherUnhandledException += (sender, e) =>
        {
            HandleException(e.Exception);
            e.Handled = true;
        };

        // catches any other exception
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            HandleException(e.ExceptionObject as Exception ?? new Exception("Something has gone wrong."));
        };

    }

    private void HandleException(Exception ex)
    {
        MessageBox.Show($"Something has gone wrong. \nError: {ex.Message}. \nStacktrace: {ex.StackTrace}");
    }
}