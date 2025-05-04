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
        //bruh

        var entity = recipeComponent.Adapt<RecipeComponentEntity>();
        await _context.RecipeComponents.AddAsync(entity);

        foreach (var alt in recipeComponent.AltIngredientsId)
        {
            var altEntity = new RecipeComponentAltIngredient()
            {
                Id = Guid.NewGuid(),
                Component = entity,
                Ingredient = await _context.Ingredients.Where(x => x.Id == alt).FirstAsync()
            };

            await _context.RecipeComponentAltIngredients.AddAsync(altEntity);
        }
    }

    public async Task<IReadOnlyCollection<RecipeComponentDto>> GetRecipeComponents(Guid recipeId)
    {
        var entities = await _context.RecipeComponents.Where(x => x.Recipe.Id == recipeId).ToListAsync();

        var alts = await _context.RecipeComponentAltIngredients.Where(x => entities.Contains(x.Component)).ToListAsync();

        var dtos = new List<RecipeComponentDto>();

        foreach (var entity in entities)
        {
            var dto = entity.Adapt<RecipeComponentDto>();

            dto.AltIngredientsId = alts.Where(x => x.Component == entity).Select(x => x.Ingredient.Id).ToList();

            dtos.Add(dto);
        }

        return dtos;
    }
}
