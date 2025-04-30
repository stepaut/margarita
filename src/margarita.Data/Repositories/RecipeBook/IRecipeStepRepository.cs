using margarita.Data.Dto.RecipeBook;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using margarita.Data.Entities.RecipeBook;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRecipeStepRepository : IRepository
{
    Task CreateRecipeStep(RecipeStepDto recipeStep);
    Task<IReadOnlyCollection<RecipeStepDto>> GetRecipeSteps(Guid recipeId);
}

internal class RecipeStepRepository : RepositoryBase, IRecipeStepRepository
{
    public RecipeStepRepository(BarDbContext context) : base(context)
    {
    }

    public async Task CreateRecipeStep(RecipeStepDto recipeStep)
    {
        var entity = recipeStep.Adapt<RecipeStepEntity>();
        await _context.RecipeSteps.AddAsync(entity);
    }

    public async Task<IReadOnlyCollection<RecipeStepDto>> GetRecipeSteps(Guid recipeId)
    {
        var entities = await _context.RecipeSteps.Where(x => x.Recipe.Id == recipeId).ToListAsync();
        return entities.Select(x => x.Adapt<RecipeStepDto>()).ToList();
    }
}