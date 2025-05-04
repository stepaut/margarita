using ReactiveUI;
using System;

namespace BetterUI.Infrastructure;

/// <summary>
/// VM, способные заменять активный контрол основного окна с меню
/// </summary>
public class SubWindowViewModelBase : ReactiveObject
{
    public event Action? ReturnBackEvent;
    private readonly IReplacerSubMainViewModel _replacer;
    private SubWindowViewModelBase? _previous;


    public SubWindowViewModelBase()
    {
        _replacer = IReplacerSubMainViewModel.GetFromApp();
    }


    protected void TurnBack()
    {
        if (ReturnBackEvent != null)
        {
            // if needs not previous, it may be custom
            ReturnBackEvent.Invoke();
        }
        else if (_previous is not null)
        {
            _replacer.Replace(_previous);
        }
    }

    public void SetPrevious(SubWindowViewModelBase previous)
    {
        _previous = previous;
    }
}
