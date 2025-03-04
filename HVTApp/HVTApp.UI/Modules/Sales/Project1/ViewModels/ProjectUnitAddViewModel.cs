using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Groups;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using ProjectUnit = HVTApp.UI.Modules.Sales.Project1.Wrappers.ProjectUnit;

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

        public ProjectUnitAddViewModel(ProjectWrapper1 projectWrapper, IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService, IGetProductService productService, IDialogService dialogService) 
            : base(new ProjectUnit(new SalesUnit()), unitOfWork, selectService, productService, dialogService)
        {
            if (projectUnit == null)
                this.ProjectUnit.PaymentConditionSet = new PaymentConditionSetEmptyWrapper(unitOfWork.Repository<PaymentConditionSet>().GetById(GlobalAppProperties.Actual.StandartPaymentsConditionSet.Id));
            else
                this.CopyProperties((ProjectUnit)ProjectUnit, projectUnit);
            
            OkCommand = new DelegateLogCommand(
            () =>
            {
                    for (int i = 0; i < Amount; i++)
                    {
                        var projectUnit1 = new ProjectUnit(new SalesUnit());
                        this.CopyProperties(projectUnit1, this.ProjectUnit);
                        projectWrapper.Units.Add(projectUnit1);
                    }
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                }, 
                () => ProjectUnit.IsValid);
            this.ProjectUnit.PropertyChanged += (sender, args) => OkCommand.RaiseCanExecuteChanged();
        }

        public new event EventHandler<DialogRequestCloseEventArgs> CloseRequested;

        private ProjectUnit CopyProperties(ProjectUnit targetProjectUnit, IProjectUnit projectUnit)
        {
            targetProjectUnit.Cost = projectUnit.Cost;
            targetProjectUnit.Comment = projectUnit.Comment;
            targetProjectUnit.CostDelivery = projectUnit.CostDelivery;
            targetProjectUnit.DeliveryDateExpected = projectUnit.DeliveryDateExpected;

            targetProjectUnit.Facility = projectUnit.Facility;
            targetProjectUnit.Product = projectUnit.Product;
            targetProjectUnit.PaymentConditionSet = projectUnit.PaymentConditionSet;
            targetProjectUnit.Producer = projectUnit.Producer;

            var pu = projectUnit is ProjectUnitGroup projectUnitGroup
                ? projectUnitGroup.Units.First()
                : projectUnit;
            var pi = pu
                .ProductsIncludedGroups
                .SelectMany(x => x.Items)
                .Select(x => x.Model)
                .Select(x => new ProductIncluded() { Product = x.Product, Amount = x.Amount, CustomFixedPrice = x.CustomFixedPrice });

            foreach (var productIncluded in pi)
            {
                targetProjectUnit.ProductsIncluded.Add(new ProjectUnitProductIncluded(productIncluded));
            }

            return targetProjectUnit;
        }
    }
}