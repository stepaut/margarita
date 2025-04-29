using BetterUI.Infrastructure;
using margarita.Data;
using margarita.MyBar;
using margarita.RecipeBook;
using margarita.Service;
using margarita.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace margarita;

public static class ServiceExtensions
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<IReplacerMainViewModel, ReplacerMainViewModel>();

        services.AddTransient<IReplacerSubMainViewModel, BarHostReplacerSubMainViewModel>();
        services.AddSingleton<BarHostViewModel>();

        services.AddServiceServices();
        services.AddDataServices();
        services.AddRecipeBookServices();
        services.AddMyBarServices();
    }
}