using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Windows.Input;

namespace BetterUI.Infrastructure;

public abstract class ActiveWindowViewModelWithMenuBase : ActiveWindowViewModelBase, ISubMainViewModel
{
    public ICommand CloseCommand { get; }

    private SubWindowViewModelBase? _activeWindow;
    public SubWindowViewModelBase? ActiveWindow
    {
        get => _activeWindow;
        set
        {
            this.RaiseAndSetIfChanged(ref _activeWindow, value);
            this.RaisePropertyChanged(nameof(ShowMenu));
        }
    }

    public bool ShowMenu => ActiveWindow is IMenuCompatible;

    [Reactive]
    public bool IsPaneOpen { get; set; } = true;

    public ICommand TriggerPaneCommand { get; }

    private readonly IReplacerMainViewModel _replacer;


    protected ActiveWindowViewModelWithMenuBase(IServiceProvider services)
    {
        _replacer = services.GetRequiredService<IReplacerMainViewModel>();

        TriggerPaneCommand = ReactiveCommand.Create(TriggerPane);
        CloseCommand = ReactiveCommand.Create(Close_Internal);
    }


    protected abstract void Close();

    private void Close_Internal()
    {
        Close();
        _replacer.ReplaceToStart();
    }

    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}
