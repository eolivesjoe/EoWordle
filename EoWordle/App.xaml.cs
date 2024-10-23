using EoWordle.Models;
using EoWordle.Services;
using EoWordle.ViewModels;
using EoWordle.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace EoWordle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
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

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<GameModel>();
            services.AddSingleton<GameView>();
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IWordService, WordService>();
        }
    }
}
