using DynamicData;
using Mapster;
using margarita.Service.RecipeBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace margarita.RecipeBook.Models;

public class RecipeBookModel
{
    public IReadOnlyCollection<RecipeFamily> RecipeFamilies => _recipeFamilies;
    private readonly List<RecipeFamily> _recipeFamilies = new();

    public IReadOnlyCollection<RecipeInfo> RecipeInfos => _recipeInfos;
    private readonly List<RecipeInfo> _recipeInfos = new();

    public IReadOnlyCollection<Ingredient> Ingredients => _ingredients;
    private readonly List<Ingredient> _ingredients = new();

    private readonly IRecipeService _recipeService;
    private readonly IIngredientService _ingredientService;
    private readonly IRecipeFamilyService _recipeFamilyService;

    public RecipeBookModel(IRecipeService recipeService, IIngredientService ingredientService,IRecipeFamilyService recipeFamilyService)
    {
        _recipeService = recipeService;
        _ingredientService = ingredientService;
        _recipeFamilyService = recipeFamilyService;
    }

    public async Task Load()
    {
        _recipeInfos.Clear();
        _recipeInfos.AddRange((await _recipeService.GetRecipeInfos()).Select(x => x.Adapt<RecipeInfo>()));

        _ingredients.Clear();
        _ingredients.AddRange((await _ingredientService.GetIngredients()).Select(x => x.Adapt<Ingredient>()));

        _recipeFamilies.Clear();
        _recipeFamilies.AddRange((await _recipeFamilyService.GetRecipeFamilies()).Select(x => x.Adapt<RecipeFamily>()));
    }

    public async Task<Recipe?> LoadRecipe(Guid id)
    {
        var recipeDto = await _recipeService.GetRecipe(id);
        if (recipeDto is null) return null;

        var family = _recipeFamilies.FirstOrDefault(x => x.Id == recipeDto.FamilyId);

        return Recipe.Create(recipeDto, family, _ingredients);
    }
}
