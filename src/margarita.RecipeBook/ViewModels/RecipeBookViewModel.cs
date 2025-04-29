using BetterUI.Infrastructure;
using margarita.Data;
using margarita.RecipeBook.Models;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeBookViewModel : SubWindowViewModelBase, IMenuCompatible
{
    private readonly RecipeBookModel _book;

    public RecipeBookViewModel(RecipeBookModel book, BarDbContext context)
    {
        _book = book;
        context.Test();
    }

    public async Task Init()
    {
        await _book.Load();
    }
}
