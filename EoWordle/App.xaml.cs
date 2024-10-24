using EoWordle.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Threading;

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

        // set up exception handling
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
        DispatcherUnhandledException += HandleUIThreadException;
        AppDomain.CurrentDomain.UnhandledException += HandleNonUIThreadException;
    }

    // handling UI thread exceptions
    private void HandleUIThreadException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        HandleException(e.Exception);
        e.Handled = true;
    }

    // handling non-UI thread exceptions
    private void HandleNonUIThreadException(object sender, UnhandledExceptionEventArgs e)
    {
        HandleException(e.ExceptionObject as Exception ?? new Exception("Something has gone wrong."));
    }

    private void HandleException(Exception ex)
    {
        MessageBox.Show($"Something has gone wrong. \nError: {ex.Message}.");
    }
}