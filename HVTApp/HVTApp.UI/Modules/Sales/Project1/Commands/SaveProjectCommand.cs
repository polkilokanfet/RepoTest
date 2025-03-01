using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class SaveProjectCommand : ICommand
    {
        private readonly ProjectWrapper1 _projectWrapper;
        private readonly IUnitOfWork _unitOfWork;
        private bool _canExecuteFlag;

        public event EventHandler CanExecuteChanged;

        private bool CanExecuteFlag
        {
            set
            {
                if (_canExecuteFlag == value) return;
                _canExecuteFlag = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public SaveProjectCommand(ProjectWrapper1 projectWrapper, IUnitOfWork unitOfWork)
        {
            _projectWrapper = projectWrapper;
            _unitOfWork = unitOfWork;
            _canExecuteFlag = CanExecute(null);
            _projectWrapper.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProjectWrapper1.IsValid) ||
                    args.PropertyName == nameof(ProjectWrapper1.IsChanged))
                    CanExecuteFlag = CanExecute(null);
            };
        }

        public bool CanExecute(object parameter)
        {
            return _projectWrapper.IsValid &&
                   _projectWrapper.IsChanged;
        }

        public void Execute(object parameter)
        {
            _projectWrapper.AcceptChanges();
            _unitOfWork.SaveEntity(_projectWrapper.Model);
            CanExecuteFlag = CanExecute(null);
        }
    }
}