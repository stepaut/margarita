using BetterUI.Infrastructure;
using margarita.RecipeBook.Models;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeBookViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public RecipeListViewModel RecipeList { get; }

    private readonly RecipeBookModel _book;

    public RecipeBookViewModel(RecipeBookModel book)
    {
        _book = book;
        RecipeList = new RecipeListViewModel(book);
    }

    public async Task Init()
    {
        await _book.Load();
        RecipeList.FillList();
    }
}
