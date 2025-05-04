using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System;
using System.Windows.Input;
using BetterUI.Infrastructure;
using margarita.RecipeBook.ViewModels;
using margarita.MyBar.ViewModels;
using System.Threading.Tasks;
using margarita.RecipeBook.Models;

namespace margarita.ViewModels;

public sealed class BarHostViewModel : ActiveWindowViewModelWithMenuBase, IMenuCompatible
{
    public ICommand ShowRecipeListCommand { get; }
    public ICommand ShowIngredientsListCommand { get; }
    public ICommand ShowRecipeFamilyListCommand { get; }
    public ICommand ShowMyBarCommand { get; }

    private readonly IReplacerSubMainViewModel _replacer;
    private readonly RecipeBookModel _book;

    public BarHostViewModel(IServiceProvider services) : base(services)
    {
        _book = services.GetRequiredService<RecipeBookModel>();
        _replacer = services.GetRequiredService<IReplacerSubMainViewModel>();

        ShowRecipeListCommand = ReactiveCommand.Create(() => Do(() => _replacer.Replace<RecipeListViewModel>(false)));
        ShowIngredientsListCommand = ReactiveCommand.Create(() => Do(() => _replacer.Replace<IngredientsListViewModel>(false)));
        ShowRecipeFamilyListCommand = ReactiveCommand.Create(() => Do(() => _replacer.Replace<RecipeFamilyListViewModel>(false)));
        ShowMyBarCommand = ReactiveCommand.Create(() => Do(() => _replacer.Replace<MyBarViewModel>(false)));
    }

    public void Init()
    {
        // bruh
        // TODO https://www.reactiveui.net/docs/handbook/when-activated
        var loadBook = ReactiveCommand.CreateFromTask(async _ =>
        {
            await _book.Reload();
            ShowRecipeListCommand.Execute(null);
        }) as ICommand;
        loadBook.Execute(null);
    }

    private static async Task DoAsync(Func<Task> func)
    {
        try
        {
            await func.Invoke();
        }
        catch (Exception ex)
        {
            //TODO log
        }
    }

    private static void Do(Action func)
    {
        try
        {
            func.Invoke();
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
