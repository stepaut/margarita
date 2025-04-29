using margarita.Data.Dto.RecipeBook;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using margarita.Data.Entities.RecipeBook;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace margarita.Data.Repositories;

public interface IRecipeStepRepository
{
    Task CreateRecipeStep(RecipeStepDto recipeStep);
    Task<IReadOnlyCollection<RecipeStepDto>> GetRecipeSteps(Guid recipeId);
}

internal class RecipeStepRepository : IRecipeStepRepository
{
    private readonly BarDbContext _context;

    public RecipeStepRepository(BarDbContext context)
    {
        _context = context;
    }

    public async Task CreateRecipeStep(RecipeStepDto recipeStep)
    {
        var entity = recipeStep.Adapt<RecipeStepEntity>();
        await _context.RecipeSteps.AddAsync(entity);
    }

    public async Task<IReadOnlyCollection<RecipeStepDto>> GetRecipeSteps(Guid recipeId)
    {
        var entities = await _context.RecipeSteps.ToListAsync();
        return entities.Select(x => x.Adapt<RecipeStepDto>()).ToList();
    }
}