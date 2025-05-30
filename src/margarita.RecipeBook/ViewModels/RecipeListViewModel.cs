﻿using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using margarita.RecipeBook.ViewModels.Editors;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels;

public class RecipeListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public ICommand CreateCommand { get; }

    [Reactive]
    public Recipe? SelectedRecipe { get; set; }

    [Reactive]
    public RecipeInfo? SelectedRecipeInfo { get; set; }

    public ReadOnlyObservableCollection<RecipeInfo> Recipes { get; }

    private readonly RecipeBookModel _book;
    private readonly IReplacerSubMainViewModel _replacer;


    public RecipeListViewModel(RecipeBookModel book, IReplacerSubMainViewModel replacer)
    {
        _book = book;
        _replacer = replacer;

        this.WhenAnyValue(x => x.SelectedRecipeInfo)
            .Subscribe(async recipeInfo => await LoadRecipe(recipeInfo?.Id));

        _book.ConnectToRecipeInfos.Bind(out var collection)
            .DisposeMany()
            .Subscribe();
        Recipes = collection;

        CreateCommand = ReactiveCommand.Create(Create);
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

    private void Create()
    {
        var editor = _replacer.Replace<RecipeEditingViewModel>();
        editor.SetPrevious(this);
    }
}
