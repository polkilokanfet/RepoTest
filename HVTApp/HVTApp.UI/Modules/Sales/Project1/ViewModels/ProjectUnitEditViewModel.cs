using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectUnitEditViewModel : BindableBase, IDialogRequestClose
    {
        private ProjectUnitProductIncludedGroup _selectedProductsIncludedGroup;
        public IProjectUnit ProjectUnit { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentConditionsSetCommand { get; }
        public ICommand ChangeProducerCommand { get; }
        public ICommand RemoveProducerCommand { get; }

        public ICommand AddProductsIncludedGroupCommand { get; }
        public ICommand RemoveProductsIncludedGroupCommand { get; }

        public ProjectUnitProductIncludedGroup SelectedProductsIncludedGroup
        {
            get => _selectedProductsIncludedGroup;
            set => SetProperty(ref _selectedProductsIncludedGroup, value);
        }

        public ProjectUnitEditViewModel(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService, IGetProductService productService, IDialogService dialogService)
        {
            ProjectUnit = projectUnit;
            ChangeFacilityCommand = new ChangeFacilityCommand(projectUnit, selectService, unitOfWork);
            ChangePaymentConditionsSetCommand = new ChangePaymentsCommand(projectUnit, selectService, unitOfWork);
            ChangeProductCommand = new ChangeProductCommand(projectUnit, productService, unitOfWork);
            ChangeProducerCommand = new ChangeProducerCommand(projectUnit, selectService, unitOfWork);
            RemoveProducerCommand = new RemoveProducerCommand(projectUnit);
            AddProductsIncludedGroupCommand = new AddProductsIncludedGroupCommand(projectUnit, productService, dialogService, unitOfWork);
            RemoveProductsIncludedGroupCommand = new RemoveProductsIncludedGroupCommand(this);
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}