using margarita.RecipeBook.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;

namespace margarita.RecipeBook.ViewModels.Items;

public class RecipeStepViewModel : ReactiveObject
{
    public Guid Id { get; }

    [Reactive]
    public string Description { get; set; } = string.Empty;

    [Reactive]
    public int Order { get; set; }

    public RecipeStepViewModel()
    {
        Id = Guid.NewGuid();
    }

    public RecipeStepViewModel(RecipeStep model)
    {
        Id = model.Id;
        Description = model.Description;
        Order = model.Order;
    }
}
