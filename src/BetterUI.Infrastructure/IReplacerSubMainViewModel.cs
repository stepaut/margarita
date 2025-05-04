using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BetterUI.Infrastructure;

public interface IReplacerSubMainViewModel
{
    T Replace<T>(bool forceReplace = true) where T : SubWindowViewModelBase;
    T Replace<T>(T vm) where T : SubWindowViewModelBase;

    static IReplacerSubMainViewModel GetFromApp()
    {
        //TODO костыль
        var services = Application.Current?.Resources[typeof(IServiceProvider)] as IServiceProvider;
        var replacer = services?.GetService<IReplacerSubMainViewModel>();
        return replacer ?? throw new Exception("Cant found in GetFromApp");
    }
}

public abstract class ReplacerSubMainViewModelBase<K> : IReplacerSubMainViewModel where K : ActiveWindowViewModelWithMenuBase
{
    private readonly IServiceProvider _services;

    public ReplacerSubMainViewModelBase(IServiceProvider services)
    {
        _services = services;
    }

    public T Replace<T>(bool forceReplace = true) where T : SubWindowViewModelBase
    {
        var host = _services.GetRequiredService<K>();
        if (!forceReplace && host.ActiveWindow is T t)
        {
            return t;
        }

        var newViewModel = _services.GetService<T>();

        host.ActiveWindow = newViewModel;

        return newViewModel ?? throw new Exception($"newViewModel is null! May be {typeof(T)} is unregistered.");
    }

    public T Replace<T>(T newViewModel) where T : SubWindowViewModelBase
    {
        var host = _services.GetRequiredService<K>();

        host.ActiveWindow = newViewModel;

        return newViewModel ?? throw new Exception($"newViewModel is null! May be {typeof(T)} is unregistered.");
    }
}
