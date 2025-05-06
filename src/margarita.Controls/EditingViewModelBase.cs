using BetterUI.Infrastructure;
using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;

namespace margarita.Controls;

public abstract class EditingViewModelBase : SubWindowViewModelBase
{
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }


    protected EditingViewModelBase()
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }


    private async Task Save()
    {
        if (!IsCorrect())
        {
            return;
        }

        await SaveImpl();

        await PostSave();

        TurnBack();
    }

    protected abstract Task SaveImpl();

    protected abstract Task PostSave();

    protected abstract bool IsCorrect();

    protected void Cancel()
    {
        TurnBack();
    }
}
