using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Windows.Input;
using UIInfrastructure;

namespace margarita.ViewModels;

public sealed class BarHostViewModel : ActiveWindowViewModelWithMenuBase, IMenuCompatible
{
    public ICommand ShowRecipeBookCommand { get; }
    public ICommand ShowMyBarCommand { get; }

    private readonly IReplacerSubMainViewModel _replacer;

    public BarHostViewModel(IServiceProvider services) : base(services)
    {
        _replacer = services.GetRequiredService<IReplacerSubMainViewModel>();
        ShowRecipeBookCommand = ReactiveCommand.Create(ShowRecipeBook);
        ShowMyBarCommand = ReactiveCommand.Create(ShowMyBar);
    }

    public void Init()
    {
        ShowRecipeBook();
    }

    private void ShowRecipeBook()
    {
        _replacer.Replace<RecipeBookViewModel>();
    }

    private void ShowMyBar()
    {
        _replacer.Replace<MyBarViewModel>();
    }

    protected override void Close()
    {
    }
}

public sealed class BarHostReplacerSubMainViewModel : ReplacerSubMainViewModelBase<BarHostViewModel>
{
    public BarHostReplacerSubMainViewModel(IServiceProvider services) : base(services)
    {
    }
}
