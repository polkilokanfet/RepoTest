using System;
using HVTApp.Model;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class OpenShippingCalculationFileCommand : BaseOpenFileCommand
    {
        public override Guid GetFileId => ViewModel.SelectedShippingCalculationFile.Id;
        public override string GetFileName => ViewModel.SelectedShippingCalculationFile.Moment.ToShortDateString();
        public override string GetFilePath => GlobalAppProperties.Actual.ShippingCostFilesPath;

        public OpenShippingCalculationFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedShippingCalculationFile != null;
        }
    }
}