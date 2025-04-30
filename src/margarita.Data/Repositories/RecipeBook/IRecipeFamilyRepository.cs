using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.Data.Entities.RecipeBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace margarita.Data.Repositories.RecipeBook;

public interface IRecipeFamilyRepository : IRepository
{
    Task CreateRecipeFamily(RecipeFamilyDto recipeFamily);
    Task<IReadOnlyCollection<RecipeFamilyDto>> GetRecipeFamilies();
}

internal class RecipeFamilyRepository : RepositoryBase, IRecipeFamilyRepository
{
    public RecipeFamilyRepository(BarDbContext context) : base(context)
    {
    }

    public async Task CreateRecipeFamily(RecipeFamilyDto recipeFamily)
    {
        var entity = recipeFamily.Adapt<RecipeFamilyEntity>();
        await _context.RecipeFamilies.AddAsync(entity);
    }

    public async Task<IReadOnlyCollection<RecipeFamilyDto>> GetRecipeFamilies()
    {
        var entities = await _context.RecipeFamilies.ToListAsync();
        return entities.Select(x => x.Adapt<RecipeFamilyDto>()).ToList();
    }
}
