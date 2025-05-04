using BetterUI.Infrastructure;
using DynamicData;
using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels;

public class IngredientsListViewModel : SubWindowViewModelBase, IMenuCompatible
{
    public ICommand CreateIngredientCommand { get; }

    [Reactive]
    public Ingredient? SelectedIngredient { get; set; }

    public ObservableCollection<Ingredient> Ingredients { get; } = new();

    private readonly RecipeBookModel _book;
    private readonly IReplacerSubMainViewModel _replacer;

    public IngredientsListViewModel(IReplacerSubMainViewModel replacer, RecipeBookModel book)
    {
        _book = book;
        _replacer = replacer;

        CreateIngredientCommand = ReactiveCommand.Create(CreateIngredient);

        Ingredients.AddRange(_book.Ingredients);
    }

    private void CreateIngredient()
    {
        var editor = _replacer.Replace<IngredientsEditingViewModel>();
        editor.SetPrevious(this);
    }
}
