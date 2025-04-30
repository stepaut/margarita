using margarita.Data.Dto.RecipeBook;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using margarita.Data.Entities.RecipeBook;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRecipeComponentRepository : IRepository
{
    Task CreateRecipeComponent(RecipeComponentDto recipeComponent);
    Task<IReadOnlyCollection<RecipeComponentDto>> GetRecipeComponents(Guid recipeId);
}

internal class RecipeComponentRepository : RepositoryBase, IRecipeComponentRepository
{
    public RecipeComponentRepository(BarDbContext context) : base(context)
    {
    }

    public async Task CreateRecipeComponent(RecipeComponentDto recipeComponent)
    {
        var entity = recipeComponent.Adapt<RecipeComponentEntity>();
        await _context.RecipeComponents.AddAsync(entity);
    }

    public async Task<IReadOnlyCollection<RecipeComponentDto>> GetRecipeComponents(Guid recipeId)
    {
        var entities = await _context.RecipeComponents.Where(x => x.Recipe.Id == recipeId).ToListAsync();
        return entities.Select(x => x.Adapt<RecipeComponentDto>()).ToList();
    }
}
