using margarita.RecipeBook.Models;
using margarita.RecipeBook.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace margarita.RecipeBook;

public static class ServiceExtensions
{
    public static void AddRecipeBookServices(this IServiceCollection services)
    {
        services.AddTransient<RecipeListViewModel>();
        services.AddTransient<RecipeFamilyListViewModel>();
        services.AddTransient<IngredientsListViewModel>();

        // editors
        services.AddTransient<IngredientsEditingViewModel>();

        // model
        services.AddSingleton<RecipeBookModel>();
    }
}