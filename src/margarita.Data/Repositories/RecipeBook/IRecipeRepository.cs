using margarita.Data.Dto.RecipeBook;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mapster;
using margarita.Data.Entities.RecipeBook;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRecipeRepository : IRepository
{
    Task CreateRecipe(RecipeDto recipe);
    Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos();
    Task<RecipeDto?> GetRecipe(Guid recipeId);
}

internal class RecipeRepository : RepositoryBase, IRecipeRepository
{

    public RecipeRepository(BarDbContext context) : base(context) { }

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
}
