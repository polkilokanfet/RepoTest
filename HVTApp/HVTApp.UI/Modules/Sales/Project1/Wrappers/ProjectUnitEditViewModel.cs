using System;
using System.ComponentModel;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.ViewModels;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitEditViewModel : BindableBase
    {
        public IProjectUnit ProjectUnit { get; }

        public ICommand ChangeFacilityCommand { get; }

        public ProjectUnitEditViewModel(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService)
        {
            ProjectUnit = projectUnit;

            ChangeFacilityCommand = new ChangeFacilityCommand(projectUnit, unitOfWork, selectService);
        }
    }

    public class ProjectUnitAddViewModel : ProjectUnitEditViewModel, IDialogRequestClose
    {
        #region Amount

        private int _amount = 1;
        public int Amount
        {
            get => _amount;
            set
            {
                if (value < 1) return;
                SetProperty(ref _amount, value);
            }
        }

        #endregion

        public DelegateLogCommand OkCommand { get; }
        public ProjectUnitGroup Result { get; private set; }

        public ProjectUnitAddViewModel(IUnitOfWork unitOfWork, ISelectService selectService) : base(new ProjectUnit(new SalesUnit()), unitOfWork, selectService)
        {
            OkCommand = new DelegateLogCommand(
                () =>
                {
                    Result = new ProjectUnitGroup();
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                },
                () => this.ProjectUnit.IsValid);
            this.ProjectUnit.PropertyChanged += (sender, args) => OkCommand.RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}