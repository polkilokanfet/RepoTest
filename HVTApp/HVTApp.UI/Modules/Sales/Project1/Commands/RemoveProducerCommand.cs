using System;
using System.Windows.Input;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class RemoveProducerCommand : ICommand
    {
        private readonly IProjectUnit _projectUnit;

        public RemoveProducerCommand(IProjectUnit projectUnit)
        {
            _projectUnit = projectUnit;
        }

        public bool CanExecute(object parameter)
        {
            return _projectUnit.Specification == null;
        }

        public void Execute(object parameter)
        {
            _projectUnit.Producer = null;
        }

        public event EventHandler CanExecuteChanged;
    }
}