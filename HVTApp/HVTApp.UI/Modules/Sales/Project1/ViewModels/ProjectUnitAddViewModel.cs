using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectUnitAddViewModel : ProjectUnitEditViewModel, IDialogRequestClose
    {
        private readonly ProjectWrapper1 _projectWrapper;

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

        public ProjectUnitAddViewModel(ProjectWrapper1 projectWrapper, IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService, IGetProductService productService, IDialogService dialogService) 
            : base(new ProjectUnit(new SalesUnit()), unitOfWork, selectService, productService, dialogService)
        {
            _projectWrapper = projectWrapper;
            if (projectUnit == null)
            {
                this.ProjectUnit.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(unitOfWork.Repository<PaymentConditionSet>().GetById(GlobalAppProperties.Actual.StandartPaymentsConditionSet.Id));
                this.ProjectUnit.Project = projectWrapper.Model;
            }
            else
            {
                ((ProjectUnit)ProjectUnit).CopyProperties(projectUnit);
            }
            
            OkCommand = new DelegateLogCommand(
            () =>
            {
                this.Add(this.ProjectUnit, Amount);
                CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            }, 
                () => ProjectUnit.IsValid);
            this.ProjectUnit.PropertyChanged += (sender, args) => OkCommand.RaiseCanExecuteChanged();
        }

        public void Add(IProjectUnit projectUnit, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var projectUnit1 = new ProjectUnit(new SalesUnit());
                projectUnit1.CopyProperties(this.ProjectUnit);
                _projectWrapper.Units.Add(projectUnit1);
            }
        }

        public new event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}