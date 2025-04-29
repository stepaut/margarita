using margarita.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using UIInfrastructure;

namespace margarita;

public static class ServiceExtensions
{
    public static void AddMyServices(this IServiceCollection services)
    {
        services.AddSingleton<MainViewModel>();
        services.AddTransient<IReplacerMainViewModel, ReplacerMainViewModel>();

        services.AddTransient<IReplacerSubMainViewModel, BarHostReplacerSubMainViewModel>();
        services.AddSingleton<BarHostViewModel>();

        services.AddTransient<RecipeBookViewModel>();
        services.AddTransient<MyBarViewModel>();
    }
}