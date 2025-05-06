using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels;

public class IngredientsListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public ICommand CreateIngredientCommand { get; }

    [Reactive]
    public Ingredient? SelectedIngredient { get; set; }

    public ReadOnlyObservableCollection<Ingredient> Ingredients { get; }

    private readonly IReplacerSubMainViewModel _replacer;

    public IngredientsListViewModel(IReplacerSubMainViewModel replacer, RecipeBookModel book)
    {
        _replacer = replacer;

        book.ConnectToIngredients.Bind(out var collection)
            .DisposeMany()
            .Subscribe();
        Ingredients = collection;

        CreateIngredientCommand = ReactiveCommand.Create(CreateIngredient);
    }

    private void CreateIngredient()
    {
        var editor = _replacer.Replace<IngredientsEditingViewModel>();
        editor.SetPrevious(this);
    }
}
