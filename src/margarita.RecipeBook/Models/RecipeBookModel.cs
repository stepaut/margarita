using DynamicData;
using Mapster;
using margarita.Service.RecipeBook;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace margarita.RecipeBook.Models;

public class RecipeBookModel
{
    public IObservable<IChangeSet<RecipeFamily, Guid>> ConnectToRecipeFamilies => _recipeFamilies.Connect();
    private readonly SourceCache<RecipeFamily, Guid> _recipeFamilies = new(x => x.Id);

    public IObservable<IChangeSet<RecipeInfo, Guid>> ConnectToRecipeInfos => _recipeInfos.Connect();
    private readonly SourceCache<RecipeInfo, Guid> _recipeInfos = new(x => x.Id);

    public IObservable<IChangeSet<Ingredient, Guid>> ConnectToIngredients => _ingredients.Connect();
    private readonly SourceCache<Ingredient, Guid> _ingredients = new(x => x.Id);

    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeFamilyService _recipeFamilyService;

    public RecipeBookModel(IRecipeService recipeService, IIngredientService ingredientService, IRecipeFamilyService recipeFamilyService)
    {
        _recipeService = recipeService;
        _ingredientService = ingredientService;
        _recipeFamilyService = recipeFamilyService;
    }

    public async Task Reload()
    {
        _recipeInfos.Clear();
        _recipeInfos.AddOrUpdate((await _recipeService.GetRecipeInfos()).Select(x => x.Adapt<RecipeInfo>()));

        _ingredients.Clear();
        _ingredients.AddOrUpdate(Ingredient.Create(await _ingredientService.GetIngredients()));

        _recipeFamilies.Clear();
        _recipeFamilies.AddOrUpdate(RecipeFamily.Create(await _recipeFamilyService.GetRecipeFamilies()));
    }

    public async Task<Recipe?> LoadRecipe(Guid id)
    {
        var recipeDto = await _recipeService.GetRecipe(id);
        if (recipeDto is null) return null;

        var family = _recipeFamilies.Items.FirstOrDefault(x => x.Id == recipeDto.FamilyId);

        return Recipe.Create(recipeDto, family, _ingredients.Items.ToList());
    }
}
