using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.Data.Entities.RecipeBook;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace margarita.Data.Repositories.RecipeBook;

public interface IIngredientRepository : IRepository
{
    Task CreateIngredient(IngredientDto ingredient);
    Task<IReadOnlyCollection<IngredientDto>> GetIngredients();
}

internal class IngredientRepository : RepositoryBase, IIngredientRepository
{
    public IngredientRepository(BarDbContext context) : base(context)
    {
    }

    public async Task CreateIngredient(IngredientDto ingredient)
    {
        var entity = ingredient.Adapt<IngredientEntity>();
        await _context.Ingredients.AddAsync(entity);
    }

    public async Task<IReadOnlyCollection<IngredientDto>> GetIngredients()
    {
        var entities = await _context.Ingredients.ToListAsync();
        return entities.Select(x => x.Adapt<IngredientDto>()).ToList();
    }
}
