using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace margarita.RecipeBook.ViewModels;

public class RecipeFamilyListViewModel : ReactiveObject
{
    [Reactive]
    public RecipeFamily? SelectedRecipeFamily { get; set; }

    public ObservableCollection<RecipeFamily> RecipeFamilies { get; } = new();

    private readonly RecipeBookModel _book;

    public RecipeFamilyListViewModel(RecipeBookModel book)
    {
        _book = book;
    }

    public void FillList()
    {
        RecipeFamilies.Clear();
        RecipeFamilies.AddRange(_book.RecipeFamilies);
    }
}
