using margarita.RecipeBook.Models;
using margarita.RecipeBook.ViewModels;
using margarita.RecipeBook.ViewModels.Editors;
using Microsoft.Extensions.DependencyInjection;

namespace margarita.RecipeBook;

public static class ServiceExtensions
{
    public static void AddRecipeBookServices(this IServiceCollection services)
    {
        // tabs
        services.AddTransient<RecipeListViewModel>();
        services.AddTransient<RecipeFamilyListViewModel>();
        services.AddTransient<IngredientsListViewModel>();

        // editors
        services.AddTransient<IngredientsEditingViewModel>();
        services.AddTransient<RecipeFamilyEditingViewModel>();
        services.AddTransient<RecipeEditingViewModel>();

        // model
        services.AddSingleton<RecipeBookModel>();
    }
}