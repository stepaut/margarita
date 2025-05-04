using BetterUI.Infrastructure;
using margarita.RecipeBook.Models;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeBookViewModel : SubWindowViewModelBase, IMenuCompatible
{
    [Reactive]
    public object SelectedTabIndex { get; set; }

    public RecipeListViewModel RecipeList { get; }
    public RecipeFamilyListViewModel RecipeFamilyList { get; }
    public IngredientsListViewModel IngredientsList { get; }

    private readonly RecipeBookModel _book;

    public RecipeBookViewModel(RecipeBookModel book)
    {
        _book = book;
        RecipeList = new RecipeListViewModel(book);
        RecipeFamilyList = new RecipeFamilyListViewModel(book);
        IngredientsList = new IngredientsListViewModel(book);
        SelectedTabIndex = 0;
    }

    public async Task Init()
    {
        await _book.Load();
        RecipeList.FillList();
        RecipeFamilyList.FillList();
        IngredientsList.FillList();
    }
}
