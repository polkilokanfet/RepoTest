using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.UI.Modules.Sales.Project1.Commands;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
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


        public ProjectUnitEditViewModel(IProjectUnit projectUnit, IUnitOfWork unitOfWork, ISelectService selectService)
        {
            ProjectUnit = projectUnit;
            ChangeFacilityCommand = new ChangeFacilityCommand(projectUnit, unitOfWork, selectService);
            RemoveProducerCommand = new RemoveProducerCommand(projectUnit);


            AddProductsIncludedGroupCommand = new DelegateCommand(
                () =>
                {

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