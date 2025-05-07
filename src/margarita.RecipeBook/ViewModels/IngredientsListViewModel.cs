using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using margarita.RecipeBook.ViewModels.Editors;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels;

public class IngredientsListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public ICommand CreateCommand { get; }

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

        CreateCommand = ReactiveCommand.Create(Create);
    }

    private void Create()
    {
        var editor = _replacer.Replace<IngredientsEditingViewModel>();
        editor.SetPrevious(this);
    }
}
