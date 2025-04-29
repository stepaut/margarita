using Microsoft.Extensions.DependencyInjection;
using System;
using BetterUI.Infrastructure;

namespace margarita.ViewModels;

public sealed class MainViewModel : MainViewModelBase
{
    public MainViewModel(IServiceProvider services) : base(services)
    {
    }

    public override void Init()
    {
        var vm = _services.GetRequiredService<BarHostViewModel>();
        vm.Init();

        ActiveWindow = vm;
    }
}

public sealed class ReplacerMainViewModel : ReplacerMainViewModelBase<MainViewModel, BarHostViewModel>
{
    public ReplacerMainViewModel(IServiceProvider services) : base(services)
    {
    }
}
