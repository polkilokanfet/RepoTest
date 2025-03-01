using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.Services;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProjectUnitEditViewModel : BindableBase
    {
        private ProjectUnitProductIncludedGroup _selectedProductsIncludedGroup;
        public IProjectUnit ProjectUnit { get; }

        public ICommand ChangeFacilityCommand { get; }
        public ICommand ChangeProductCommand { get; }
        public ICommand ChangePaymentConditionsSetCommand { get; }
        public ICommand ChangeProducerCommand { get; }
        public ICommand RemoveProducerCommand { get; }

        public ProjectUnitProductIncludedGroup SelectedProductsIncludedGroup
        {
            get => _selectedProductsIncludedGroup;
            set => SetProperty(ref _selectedProductsIncludedGroup, value, () => ((DelegateCommand)RemoveProductsIncludedGroupCommand).RaiseCanExecuteChanged());
        }

        public ICommand AddProductsIncludedGroupCommand { get; }
        public ICommand RemoveProductsIncludedGroupCommand { get; }


        public ProjectUnitEditViewModel(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService, IGetProductService getProductService, IDialogService dialogService)
        {
            ProjectUnit = projectUnit;
            ChangeFacilityCommand = new ChangeFacilityCommand(projectUnit, unitOfWork, selectService);
            RemoveProducerCommand = new RemoveProducerCommand(projectUnit);


            AddProductsIncludedGroupCommand = new DelegateCommand(
                () =>
                {
                    var viewModel = new ProductIncludedViewModel(unitOfWork, getProductService);
                    var dr = dialogService.ShowDialog(viewModel);
                    if (dr == true)
                    {
                        
                    }
                });

            RemoveProductsIncludedGroupCommand = new DelegateCommand(
                () =>
                {
                    var targetGroup = this.SelectedProductsIncludedGroup;
                    this.SelectedProductsIncludedGroup = null;

                    foreach (var targetProductIncluded in targetGroup.Items)
                    {
                        this.ProjectUnit.RemoveProductIncluded(targetProductIncluded);
                    }
                },
                () => this.SelectedProductsIncludedGroup != null);

        }
    }
}