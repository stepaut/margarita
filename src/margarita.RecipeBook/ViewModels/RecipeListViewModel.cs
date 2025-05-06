using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    [Reactive]
    public Recipe? SelectedRecipe { get; set; }

    [Reactive]
    public RecipeInfo? SelectedRecipeInfo { get; set; }

    public ReadOnlyObservableCollection<RecipeInfo> Recipes { get; }

    private readonly RecipeBookModel _book;

    public RecipeListViewModel(RecipeBookModel book)
    {
        _book = book;

        this.WhenAnyValue(x => x.SelectedRecipeInfo)
            .Subscribe(async recipeInfo => await LoadRecipe(recipeInfo?.Id));

        _book.ConnectToRecipeInfos.Bind(out var collection)
            .DisposeMany()
            .Subscribe();
        Recipes = collection;
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
