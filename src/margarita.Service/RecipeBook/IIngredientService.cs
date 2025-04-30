using margarita.Data.Dto.RecipeBook;
using margarita.Data.Repositories.RecipeBook;

namespace margarita.Service.RecipeBook;

public interface IIngredientService
{
    Task CreateIngredient(IngredientDto ingredient);
    Task<IReadOnlyCollection<IngredientDto>> GetIngredients();
}

internal class IngredientService : IIngredientService
{
    private readonly Cache<IngredientDto> _ingredients = new();
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientService(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }

    public async Task CreateIngredient(IngredientDto ingredient)
    {
        await _ingredientRepository.CreateIngredient(ingredient);
        _ingredients.Create(ingredient.Id, ingredient);

        await _ingredientRepository.Save();
    }

    public async Task<IReadOnlyCollection<IngredientDto>> GetIngredients()
    {
        var cached = _ingredients.GetAll();
        if (cached.Any()) return cached;

        cached = await _ingredientRepository.GetIngredients();
        foreach (var item in cached)
        {
            _ingredients.Create(item.Id, item);
        }
        return cached;
    }
}