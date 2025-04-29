using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeListViewModel : ReactiveObject
{
    [Reactive]
    public Recipe? SelectedRecipe { get; set; }

    [Reactive]
    public RecipeInfo? SelectedRecipeInfo { get; set; }

    public ObservableCollection<RecipeInfo> Recipes { get; } = new();

    private readonly RecipeBookModel _book;

    public RecipeListViewModel(RecipeBookModel book)
    {
        _book = book;

        this.WhenAnyValue(x => x.SelectedRecipeInfo)
            .Subscribe(async recipeInfo => await LoadRecipe(recipeInfo?.Id));
    }

    public void FillList()
    {
        Recipes.Clear();
        Recipes.AddRange(_book.Recipes);
    }

    private async Task LoadRecipe(Guid? recipeId)
    {
        if (recipeId is null)
        {
            SelectedRecipe = null;
            return;
        }

        SelectedRecipe = await _book.LoadRecipe(recipeId.Value);
    }
}
