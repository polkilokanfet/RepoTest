using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public abstract class ProjectUnitEditUnitOfWorkBaseCommand : ProjectUnitEditBaseCommand
    {
        protected IUnitOfWork UnitOfWork { get; }

        protected ProjectUnitEditUnitOfWorkBaseCommand(IProjectUnit projectUnit, IUnitOfWork unitOfWork) : base(projectUnit)
        {
            UnitOfWork = unitOfWork;
        }
    }

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