using margarita.Data.Repositories;
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
    }
}