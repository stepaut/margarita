using DynamicData;
using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.RecipeBook.Models;
using margarita.Service.RecipeBook;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace margarita.RecipeBook.ViewModels.Editors;

public class RecipeEditingViewModel : BookEditingViewModelBase
{
    public ICommand CreateComponentCommand { get; }
    public ICommand CreateStepCommand { get; }

    [Reactive]
    public string OriginalName { get; set; }

    public ObservableCollection<RecipeComponent> Components { get; } = new();

    public ObservableCollection<RecipeStep> Steps { get; } = new();

    [Reactive]
    public RecipeFamily? Family { get; set; }
    public ObservableCollection<RecipeFamily?> RecipeFamilies { get; }

    private readonly IRecipeService _service;


    public RecipeEditingViewModel(IRecipeService service, RecipeBookModel book) : base(book)
    {
        _service = service;

        book.ConnectToRecipeFamilies.Bind(out var recipeFamilies)
            .DisposeMany()
            .Subscribe();
        RecipeFamilies = [null, .. recipeFamilies];
    }


    public void SetSource(Recipe source)
    {
        Id = source.Id;
        Name = source.Name;
        OriginalName = source.OriginalName;
        Description = source.Description;
        Family = source.Family;
    }

    protected override async Task SaveImpl()
    {
        var dto = this.Adapt<RecipeDto>();
        dto.FamilyId = Family?.Id;

        await _service.CreateRecipe(dto, null, null);
    }

    protected override bool IsCorrect()
    {
        return base.IsCorrect() && Components.Count > 0;
    }
}
