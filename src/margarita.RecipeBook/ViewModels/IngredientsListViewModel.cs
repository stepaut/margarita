using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace margarita.RecipeBook.ViewModels;

public class IngredientsListViewModel : ReactiveObject
{
    [Reactive]
    public Ingredient? SelectedIngredient { get; set; }

    public ObservableCollection<Ingredient> Ingredients { get; } = new();

    private readonly RecipeBookModel _book;

    public IngredientsListViewModel(RecipeBookModel book)
    {
        _book = book;
    }

    public void FillList()
    {
        Ingredients.Clear();
        Ingredients.AddRange(_book.Ingredients);
    }
}
