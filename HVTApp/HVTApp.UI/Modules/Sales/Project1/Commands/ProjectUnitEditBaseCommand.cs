using System;
using System.Windows.Input;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public abstract class ProjectUnitEditBaseCommand : ICommand
    {
        protected readonly IProjectUnit ProjectUnit;

        protected ProjectUnitEditBaseCommand(IProjectUnit projectUnit)
        {
            ProjectUnit = projectUnit;
        }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged;
    }
}