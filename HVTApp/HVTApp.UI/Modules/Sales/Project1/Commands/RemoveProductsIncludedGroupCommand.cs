using HVTApp.UI.Modules.Sales.Project1.ViewModels;

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
            foreach (var targetProductIncluded in _viewModel.SelectedProductsIncludedGroup.Items)
            {
                _viewModel.ProjectUnit.RemoveProductIncluded(targetProductIncluded);
            }

            _viewModel.SelectedProductsIncludedGroup = null;
        }
    }
}