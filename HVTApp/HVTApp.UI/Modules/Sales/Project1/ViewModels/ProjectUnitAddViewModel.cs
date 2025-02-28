using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
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
        public IEnumerable<ProjectUnit> Result { get; private set; }

        public ProjectUnitAddViewModel(IUnitOfWork unitOfWork, ISelectService selectService, IGetProductService getProductService, IDialogService dialogService) 
            : base(new ProjectUnit(new SalesUnit()), unitOfWork, selectService, getProductService, dialogService)
        {
            OkCommand = new DelegateLogCommand(
                () =>
                {
                    Result = CloneProjectUnits(this.ProjectUnit, this.Amount);
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                });
            this.ProjectUnit.PropertyChanged += (sender, args) => OkCommand.RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        /// <summary>
        /// Клонирование по образцу.
        /// </summary>
        /// <param name="projectUnit"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private IEnumerable<ProjectUnit> CloneProjectUnits(IProjectUnit projectUnit, int amount)
        {
            for (var i = 0; i < amount; i++)
            {
                yield return new ProjectUnit(projectUnit);
            }
        }

    }
}