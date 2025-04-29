using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Windows.Input;
using BetterUI.Infrastructure;
using margarita.RecipeBook.ViewModels;
using margarita.MyBar.ViewModels;
using System.Threading.Tasks;

namespace margarita.ViewModels;

public sealed class BarHostViewModel : ActiveWindowViewModelWithMenuBase, IMenuCompatible
{
    public ICommand ShowRecipeBookCommand { get; }
    public ICommand ShowMyBarCommand { get; }

    private readonly IReplacerSubMainViewModel _replacer;

    public BarHostViewModel(IServiceProvider services) : base(services)
    {
        _replacer = services.GetRequiredService<IReplacerSubMainViewModel>();
        ShowRecipeBookCommand = ReactiveCommand.CreateFromTask(ShowRecipeBook);
        ShowMyBarCommand = ReactiveCommand.Create(ShowMyBar);
    }

    public void Init()
    {
        ShowRecipeBookCommand.Execute(null);
    }

    private async Task ShowRecipeBook()
    {
        try
        {
            var vm = _replacer.Replace<RecipeBookViewModel>();
            await vm.Init();
        }
        catch (Exception ex)
        {
            //TODO log
        }
    }

    private void ShowMyBar()
    {
        try
        {
            _replacer.Replace<MyBarViewModel>();
        }
        catch (Exception ex)
        {
            //TODO log
        }
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
