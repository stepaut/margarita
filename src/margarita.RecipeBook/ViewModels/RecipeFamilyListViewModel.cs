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

public class RecipeFamilyListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public ICommand CreateCommand { get; }

    [Reactive]
    public RecipeFamily? SelectedRecipeFamily { get; set; }

    public ReadOnlyObservableCollection<RecipeFamily> RecipeFamilies { get; }

    private readonly IReplacerSubMainViewModel _replacer;

    public RecipeFamilyListViewModel(IReplacerSubMainViewModel replacer, RecipeBookModel book)
    {
        _replacer = replacer;

        book.ConnectToRecipeFamilies.Bind(out var recipeFamilies)
            .DisposeMany()
            .Subscribe();
        RecipeFamilies = recipeFamilies;

        CreateCommand = ReactiveCommand.Create(Create);
    }

    private void Create()
    {
        var editor = _replacer.Replace<RecipeFamilyEditingViewModel>();
        editor.SetPrevious(this);
    }
}
