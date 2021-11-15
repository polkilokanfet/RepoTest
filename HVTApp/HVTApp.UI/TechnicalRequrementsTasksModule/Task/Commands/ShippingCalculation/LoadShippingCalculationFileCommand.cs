using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadShippingCalculationFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public LoadShippingCalculationFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var storageDirectory = GlobalAppProperties.Actual.ShippingCostFilesPath;
            FilesStorageService.CopyFileFromStorage(ViewModel.SelectedShippingCalculationFile.Id, storageDirectory);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedShippingCalculationFile != null;
        }
    }
}