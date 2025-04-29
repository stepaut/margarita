using margarita.Data.Dto.RecipeBook;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mapster;
using margarita.Data.Entities.RecipeBook;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRecipeRepository
{
    Task CreateRecipe(RecipeDto recipe);
    Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos();
    Task<RecipeDto?> GetRecipe(Guid recipeId);
    Task Save();
}

internal class RecipeRepository : IRecipeRepository
{
    private readonly BarDbContext _context;

    public RecipeRepository(BarDbContext context)
    {
        _context = context;
    }

    public async Task CreateRecipe(RecipeDto recipe)
    {
        var entity = recipe.Adapt<RecipeEntity>();
        await _context.Recipes.AddAsync(entity);
    }

    public async Task<RecipeDto?> GetRecipe(Guid recipeId)
    {
        var entity = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == recipeId);
        var dto = entity?.Adapt<RecipeDto>();
        return dto;
    }

    public async Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos()
    {
        var entities = await _context.Recipes.ToListAsync();
        return entities.Select(x => x.Adapt<RecipeInfoDto>()).ToList();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}