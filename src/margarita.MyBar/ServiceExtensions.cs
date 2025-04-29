using margarita.MyBar.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace margarita.MyBar;

public static class ServiceExtensions
{
    public static void AddMyBarServices(this IServiceCollection services)
    {
        services.AddTransient<MyBarViewModel>();
    }
}