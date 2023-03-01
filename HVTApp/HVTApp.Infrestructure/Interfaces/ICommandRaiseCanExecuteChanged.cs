using System.Windows.Input;

namespace HVTApp.Infrastructure.Interfaces
{
    public interface ICommandRaiseCanExecuteChanged : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}