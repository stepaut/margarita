using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Threading.Tasks;

namespace BetterUI.Infrastructure;

public abstract class MainViewModelBase : ReactiveObject
{
    [Reactive]
    public ActiveWindowViewModelBase? ActiveWindow { get; set; }

    protected readonly IServiceProvider _services;

    public MainViewModelBase(IServiceProvider services)
    {
        _services = services;
    }

    public abstract void Init();
}

