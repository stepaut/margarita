using margarita.Service.RecipeBook;
using Microsoft.Extensions.DependencyInjection;

namespace margarita.Service;

public static class ServiceExtensions
{
    public static void AddServiceServices(this IServiceCollection services)
    {
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IIngredientService, IngredientService>();
    }
}