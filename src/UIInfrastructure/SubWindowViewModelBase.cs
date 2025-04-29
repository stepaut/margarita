using ReactiveUI;
using System;

namespace UIInfrastructure;

/// <summary>
/// VM, способные заменять активный контрол основного окна с меню
/// </summary>
public class SubWindowViewModelBase : ReactiveObject
{
    internal event Action? ReturnBackEvent;

    public ISubMainViewModel Main { get; }

    private readonly SubWindowViewModelBase? _previous;


    //public SubWindowViewModelBase(ISubMainViewModel main)
    //{
    //    //Main = main;
    //    //_previous = Main.ActiveWindow;
    //}


    protected void TurnBack()
    {
        if (ReturnBackEvent != null)
        {
            // if needs not previous, it may be custom
            ReturnBackEvent.Invoke();
        }
        else
        {
            Main.ActiveWindow = _previous;
        }
    }
}
