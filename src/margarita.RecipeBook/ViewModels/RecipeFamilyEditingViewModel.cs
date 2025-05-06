using Mapster;
using margarita.Data.Dto.RecipeBook;
using margarita.RecipeBook.Models;
using margarita.Service.RecipeBook;
using ReactiveUI.Fody.Helpers;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels;

public class RecipeFamilyEditingViewModel : BookEditingViewModelBase
{
    [Reactive]
    public RecipeFamily? Parent { get; set; }

    private readonly IRecipeFamilyService _service;


    public RecipeFamilyEditingViewModel(IRecipeFamilyService service, RecipeBookModel book) : base(book)
    {
        _service = service;
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
