using margarita.Data.Repositories.RecipeBook;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace margarita.Data;

public static class ServiceExtensions
{
    public static void AddDataServices(this IServiceCollection services)
    {
        Directory.CreateDirectory("C:\\margarita");
        services.AddDbContext<BarDbContext>(options => options.UseSqlite("Data Source=C:\\margarita\\margarita.db"));

        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();
        services.AddScoped<IRecipeComponentRepository, RecipeComponentRepository>();
        services.AddScoped<IRecipeFamilyRepository, RecipeFamilyRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
    }
}