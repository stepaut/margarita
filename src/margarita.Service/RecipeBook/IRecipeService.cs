using margarita.Data.Dto.RecipeBook;
using margarita.Data.Repositories.RecipeBook;

namespace margarita.Service.RecipeBook;

public interface IRecipeService
{
    Task CreateRecipe(RecipeDto recipe, IReadOnlyCollection<RecipeStepDto> steps, IReadOnlyCollection<RecipeComponentDto> components);
    Task<IReadOnlyCollection<RecipeInfoDto>> GetRecipeInfos();
    Task<RecipeDto?> GetRecipe(Guid recipeId);
}

internal class RecipeService : IRecipeService
{
    private readonly Cache<RecipeInfoDto> _recipeInfos = new();
    private readonly Cache<RecipeDto> _recipes = new();

    private readonly IRecipeRepository _recipeRepository;
    private readonly IRecipeStepRepository _recipeStepRepository;
    private readonly IRecipeComponentRepository _recipeComponentRepository;

    public RecipeService(IRecipeRepository recipeRepository, IRecipeStepRepository recipeStepRepository, IRecipeComponentRepository recipeComponentRepository)
    {
        _recipeRepository = recipeRepository;
        _recipeStepRepository = recipeStepRepository;
        _recipeComponentRepository = recipeComponentRepository;
    }

    public async Task CreateRecipe(RecipeDto recipe, IReadOnlyCollection<RecipeStepDto> steps, IReadOnlyCollection<RecipeComponentDto> components)
    {
        foreach (var step in steps)
        {
            await _recipeStepRepository.CreateRecipeStep(step);
        }

        foreach (var component in components)
        {
            await _recipeComponentRepository.CreateRecipeComponent(component);
        }

        await _recipeRepository.CreateRecipe(recipe);
        _recipes.Create(recipe.Id, recipe);

        await _recipeRepository.Save();
    }

    public async Task<RecipeDto?> GetRecipe(Guid recipeId)
    {
        var cached = _recipes.Get(recipeId);
        if (cached is not null) return cached;

        cached = await _recipeRepository.GetRecipe(recipeId);
        if (cached is null) return null;

        var steps = await _recipeStepRepository.GetRecipeSteps(recipeId);
        foreach (var step in steps)
        {
            cached.Steps.Add(step);
        }

        var components = await _recipeComponentRepository.GetRecipeComponents(recipeId);
        foreach (var component in components)
        {
            cached.Components.Add(component);
        }

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
