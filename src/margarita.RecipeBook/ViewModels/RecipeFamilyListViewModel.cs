using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace margarita.RecipeBook.ViewModels;

public class RecipeFamilyListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    [Reactive]
    public RecipeFamily? SelectedRecipeFamily { get; set; }

    public ObservableCollection<RecipeFamily> RecipeFamilies { get; } = new();

    private readonly RecipeBookModel _book;

    public RecipeFamilyListViewModel(RecipeBookModel book)
    {
        _book = book;

        RecipeFamilies.AddRange(_book.RecipeFamilies);
    }
}
