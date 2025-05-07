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

public class RecipeFamilyEditingViewModel : BookEditingViewModelBase
{
    [Reactive]
    public RecipeFamily? Parent { get; set; }
    public ObservableCollection<RecipeFamily?> RecipeFamilies { get; }


    private readonly IRecipeFamilyService _service;


    public RecipeFamilyEditingViewModel(IRecipeFamilyService service, RecipeBookModel book) : base(book)
    {
        _service = service;

        book.ConnectToRecipeFamilies.Bind(out var recipeFamilies)
            .DisposeMany()
            .Subscribe();
        RecipeFamilies = [null, .. recipeFamilies];
    }


    public void SetSource(RecipeFamily source)
    {
        Id = source.Id;
        Name = source.Name;
        Description = source.Description;
        Parent = source.Parent;
    }

    protected override async Task SaveImpl()
    {
        var dto = this.Adapt<RecipeFamilyDto>();
        dto.ParentId = Parent?.Id;

        await _service.CreateRecipeFamily(dto);
    }
}
