using Microsoft.Extensions.DependencyInjection;
using System;

namespace UIInfrastructure;

public interface IReplacerSubMainViewModel
{
    T Replace<T>() where T : SubWindowViewModelBase;
}

public abstract class ReplacerSubMainViewModelBase<K> : IReplacerSubMainViewModel where K : ActiveWindowViewModelWithMenuBase
{
    private readonly IServiceProvider _services;

    public ReplacerSubMainViewModelBase(IServiceProvider services)
    {
        _services = services;
    }

    public T Replace<T>() where T : SubWindowViewModelBase
    {
        var host = _services.GetRequiredService<K>();

        var newViewModel = _services.GetService<T>();

        host.ActiveWindow = newViewModel;

        return newViewModel ?? throw new Exception($"newViewModel is null! May be {typeof(T)} is unregistered.");
    }
}
