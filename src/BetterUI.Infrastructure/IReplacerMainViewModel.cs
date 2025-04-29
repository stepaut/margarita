using Microsoft.Extensions.DependencyInjection;
using System;

namespace UIInfrastructure;

public interface IReplacerMainViewModel
{
    T Replace<T>() where T : ActiveWindowViewModelBase;

    void ReplaceToStart();
}

public abstract class ReplacerMainViewModelBase<TMVM, TSMVM> : IReplacerMainViewModel where TMVM : MainViewModelBase where TSMVM : ActiveWindowViewModelWithMenuBase
{
    private readonly IServiceProvider _services;

    public ReplacerMainViewModelBase(IServiceProvider services)
    {
        _services = services;
    }

    public T Replace<T>() where T : ActiveWindowViewModelBase
    {
        var mainViewModel = _services.GetRequiredService<TMVM>();
        if (mainViewModel.ActiveWindow is T existed)
        {
            return existed;
        }

        var newViewModel = _services.GetService<T>();

        mainViewModel.ActiveWindow = newViewModel;

        return newViewModel ?? throw new Exception();
    }

    public void ReplaceToStart()
    {
        Replace<TSMVM>();
    }
}

