using margarita.Controls;
using margarita.RecipeBook.Models;
using ReactiveUI.Fody.Helpers;
using System;
using System.Threading.Tasks;

namespace margarita.RecipeBook.ViewModels.Editors;

public abstract class BookEditingViewModelBase : EditingViewModelBase
{
    [Reactive]
    public Guid Id { get; protected set; }

    [Reactive]
    public string Name { get; set; } = string.Empty;

    [Reactive]
    public string Description { get; set; } = string.Empty;

    private readonly RecipeBookModel _book;

    public BookEditingViewModelBase(RecipeBookModel book)
    {
        _book = book;

        Id = Guid.NewGuid();
    }

    protected override async Task PostSave()
    {
        await _book.Reload();
    }

    protected override bool IsCorrect()
    {
        return Id != Guid.Empty && !string.IsNullOrEmpty(Name);
    }
}
