using margarita.Data.Dto.RecipeBook;
using margarita.Data.Repositories.RecipeBook;

namespace margarita.Service.RecipeBook;

public interface IRecipeFamilyService
{
    Task CreateRecipeFamily(RecipeFamilyDto recipeFamily);
    Task<IReadOnlyCollection<RecipeFamilyDto>> GetRecipeFamilies();
}

internal class RecipeFamilyService : IRecipeFamilyService
{
    private readonly Cache<RecipeFamilyDto> _recipeFamilies = new();
    private readonly IRecipeFamilyRepository _recipeFamilyRepository;

    public RecipeFamilyService(IRecipeFamilyRepository recipeFamilyRepository)
    {
        _recipeFamilyRepository = recipeFamilyRepository;
    }

    public async Task CreateRecipeFamily(RecipeFamilyDto recipeFamily)
    {
        await _recipeFamilyRepository.CreateRecipeFamily(recipeFamily);
        _recipeFamilies.Create(recipeFamily.Id, recipeFamily);

        await _recipeFamilyRepository.Save();
    }

    public async Task<IReadOnlyCollection<RecipeFamilyDto>> GetRecipeFamilies()
    {
        var cached = _recipeFamilies.GetAll();
        if (cached.Any()) return cached;

        cached = await _recipeFamilyRepository.GetRecipeFamilies();
        foreach (var item in cached)
        {
            _recipeFamilies.Create(item.Id, item);
        }
        return cached;
    }
}
