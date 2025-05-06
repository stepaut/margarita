using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;

namespace margarita.RecipeBook.ViewModels;

public class RecipeFamilyListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    [Reactive]
    public RecipeFamily? SelectedRecipeFamily { get; set; }

    public ReadOnlyObservableCollection<RecipeFamily> RecipeFamilies { get; }

    public RecipeFamilyListViewModel(RecipeBookModel book)
    {
        book.ConnectToRecipeFamilies.Bind(out var recipeFamilies)
            .DisposeMany()
            .Subscribe();
        RecipeFamilies = recipeFamilies;
    }
}
