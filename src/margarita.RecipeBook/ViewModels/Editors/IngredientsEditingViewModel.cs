using DynamicData;
using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.RecipeBook.Models;
using margarita.Service.RecipeBook;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels.Editors;

public class IngredientsEditingViewModel : BookEditingViewModelBase
{
    [Reactive]
    public int Alc { get; set; } = 0;

    [Reactive]
    public Ingredient? Parent { get; set; }
    public ObservableCollection<Ingredient?> Ingredients { get; }


    private readonly IIngredientService _service;


    public IngredientsEditingViewModel(IIngredientService service, RecipeBookModel book) : base(book)
    {
        _service = service;

        book.ConnectToIngredients.Bind(out var ingredients)
            .DisposeMany()
            .Subscribe();
        Ingredients = [null, .. ingredients];
    }


    public void SetSource(Ingredient source)
    {
        Id = source.Id;
        Name = source.Name;
        Description = source.Description;
        Alc = source.Alc;
        Parent = source.Parent;
    }

    protected override async Task SaveImpl()
    {
        var dto = this.Adapt<IngredientDto>();
        dto.ParentId = Parent?.Id;

        await _service.CreateOrUpdateIngredient(dto);
    }
}
