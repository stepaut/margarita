using Mapster;
using margarita.Service.RecipeBook;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace margarita.RecipeBook.Models;

public class RecipeBookModel
{
    public IReadOnlyCollection<RecipeInfo> Recipes => _recipes;
    private readonly List<RecipeInfo> _recipes = new();

    public IReadOnlyCollection<IngredientInfo> Ingredients => _ingredients;
    private readonly List<IngredientInfo> _ingredients = new();

    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;

    public RecipeBookModel(IRecipeService recipeService, IIngredientService ingredientService)
    {
        _recipeService = recipeService;
        _ingredientService = ingredientService;
    }

    public async Task Load()
    {
        _recipes.Clear();
        _recipes.AddRange((await _recipeService.GetRecipeInfos()).Select(x => x.Adapt<RecipeInfo>()));

        _ingredients.Clear();
        _ingredients.AddRange((await _ingredientService.GetIngredientInfos()).Select(x => x.Adapt<IngredientInfo>()));
    }
}
