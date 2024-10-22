using EoWordle.Models;
using EoWordle.Services;
using EoWordle.ViewModels;
using EoWordle.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace EoWordle
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Set up DI for the project
        public App()
        {
            ServiceCollection serviceColletion = new();
            serviceColletion.ConfigureServices();

            ServiceProvider serviceProvider = serviceColletion.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<GameViewModel>();
            services.AddSingleton<GameModel>();
            services.AddSingleton<GameView>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IGameService, GameService>();
            services.AddSingleton<IWordService, WordService>();
        }
    }
}
