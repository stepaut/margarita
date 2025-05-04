using BetterUI.Infrastructure;
using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.RecipeBook.Models;
using margarita.Service.RecipeBook;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels;

public abstract class EditingViewModelBase : SubWindowViewModelBase
{
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }


    protected EditingViewModelBase()
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }


    public abstract Task Save();

    public void Cancel()
    {
        TurnBack();
    }
}

public class IngredientsEditingViewModel : EditingViewModelBase
{
    [Reactive]
    public Guid Id { get; private set; }

    [Reactive]
    public string Name { get; set; } = string.Empty;

    [Reactive]
    public string Description { get; set; } = string.Empty;

    [Reactive]
    public int Alc { get; set; } = 0;

    [Reactive]
    public Ingredient? Parent { get; set; }

    private readonly RecipeBookModel _book;
    private readonly IIngredientService _ingredientService;


    public IngredientsEditingViewModel(IIngredientService ingredientService, RecipeBookModel book)
    {
        _ingredientService = ingredientService;
        Id = Guid.NewGuid();
        _book = book;
    }


    public void SetSource(Ingredient ingredient)
    {
        Id = ingredient.Id;
        Name = ingredient.Name;
        Description = ingredient.Description;
        Alc = ingredient.Alc;
        Parent = ingredient.Parent;
    }

    public override async Task Save()
    {
        if (!IsCorrect())
        {
            return;
        }

        var dto = this.Adapt<IngredientDto>();
        dto.ParentId = Parent?.Id;

        await _ingredientService.CreateOrUpdateIngredient(dto);

        await _book.Reload();

        TurnBack();
    }

    private bool IsCorrect()
    {
        return Id != Guid.Empty && !string.IsNullOrEmpty(Name);
    }
}
