using margarita.Data.Dto.RecipeBook;
using margarita.Data.Repositories;

namespace margarita.Service.RecipeBook;

public interface IRecipeService
{
    Task CreateRecipe(RecipeDto recipe, IReadOnlyCollection<RecipeStepDto> steps);
    Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos();
    Task<RecipeDto> GetRecipe(Guid recipeId);
}

internal class RecipeService : IRecipeService
{
    private readonly Cache<RecipeInfoDto> _recipeInfos = new();
    private readonly Cache<RecipeDto> _recipes = new();
    private readonly Cache<RecipeStepDto> _recipeSteps = new();
    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeStepRepository _recipeStepRepository;

    public RecipeService(IRecipeRepository recipeRepository, IRecipeStepRepository recipeStepRepository)
    {
        _recipeRepository = recipeRepository;
        _recipeStepRepository = recipeStepRepository;
    }

    public async Task CreateRecipe(RecipeDto recipe, IReadOnlyCollection<RecipeStepDto> steps)
    {
        foreach (var step in steps)
        {
            await _recipeStepRepository.CreateRecipeStep(step);
            _recipeSteps.Create(step.Id, step);
        }

        await _recipeRepository.CreateRecipe(recipe);
        _recipes.Create(recipe.Id, recipe);

        await _recipeRepository.Save();
    }

    public async Task<RecipeDto> GetRecipe(Guid recipeId)
    {
        var cached = _recipes.Get(recipeId);
        if (cached is not null) return cached;

        cached = await _recipeRepository.GetRecipe(recipeId);
        _recipes.Create(recipeId, cached);
        return cached;
    }

    public async Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos()
    {
        var cached = _recipeInfos.GetAll();
        if (cached.Any()) return cached;

        cached = await _recipeRepository.GetRecipeInfos();
        foreach (var info in cached)
        {
            _recipeInfos.Create(info.Id, info);
        }
        return cached;
    }
}
