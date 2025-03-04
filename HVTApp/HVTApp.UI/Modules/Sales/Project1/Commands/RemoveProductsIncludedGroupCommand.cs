using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public class RemoveProductsIncludedGroupCommand : RaiseCanExecuteChangedCommand
    {
        private readonly ProjectUnitEditViewModel _viewModel;

        public RemoveProductsIncludedGroupCommand(ProjectUnitEditViewModel viewModel)
        {
            _viewModel = viewModel;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(ProjectUnitEditViewModel.SelectedProductsIncludedGroup))
                {
                    RaiseCanExecuteChanged();
                }
            };
        }

        public override bool CanExecute(object parameter)
        {
            return _viewModel.SelectedProductsIncludedGroup != null;
        }

        public override void Execute(object parameter)
        {
            foreach (var productIncluded in _viewModel.SelectedProductsIncludedGroup.Items)
            {
                if (_viewModel.ProjectUnit is ProjectUnit projectUnit) 
                    RemoveProductIncluded(projectUnit, productIncluded);
                else if (_viewModel.ProjectUnit is ProjectUnitGroup projectUnitGroup)
                    RemoveProductIncluded(projectUnitGroup, productIncluded);
            }

            _viewModel.SelectedProductsIncludedGroup = null;
        }

        private void RemoveProductIncluded(ProjectUnit projectUnit, ProjectUnitProductIncluded productIncluded)
        {
            projectUnit.ProductsIncluded.Remove(productIncluded);
        }

        private void RemoveProductIncluded(ProjectUnitGroup projectUnitGroup, ProjectUnitProductIncluded productIncluded)
        {
            foreach (var projectUnit in projectUnitGroup.Units)
            {
                RemoveProductIncluded(projectUnit, productIncluded);
            }
        }
    }
}