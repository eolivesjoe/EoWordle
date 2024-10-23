using EoWordle.Models;
using EoWordle.Services;
using EoWordle.ViewModels;
using EoWordle.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EoWordle;

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