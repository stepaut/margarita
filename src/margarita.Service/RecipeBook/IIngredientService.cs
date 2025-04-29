using margarita.Data.Dto.RecipeBook;

namespace margarita.Service.RecipeBook;

public interface IIngredientService
{
    Task CreateIngredient(IngredientDto ingredient);
    Task<IReadOnlyCollection<IngredientInfoDto>> GetIngredientInfos();
    Task<IngredientDto> GetIngredient(Guid ingredientId);
}

internal class IngredientService : IIngredientService
{
    public async Task CreateIngredient(IngredientDto ingredient)
    {
        throw new NotImplementedException();
    }

    public async Task<IngredientDto> GetIngredient(Guid ingredientId)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyCollection<IngredientInfoDto>> GetIngredientInfos()
    {
        return new List<IngredientInfoDto>();
    }
}