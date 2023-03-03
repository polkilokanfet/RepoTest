using System.Windows.Input;

namespace HVTApp.Infrastructure.Interfaces
{
    public interface ICommandRaiseCanExecuteChanged : ICommand
    {
        void RaiseCanExecuteChanged();
    }
    public interface IIsVisible
    {
        bool IsVisible { get; }
    }
    public interface ICommandIsVisibleWhenCanExecute : IIsVisible, ICommandRaiseCanExecuteChanged
    { }

}