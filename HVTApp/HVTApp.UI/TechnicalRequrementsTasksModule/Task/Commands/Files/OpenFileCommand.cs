using System;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class OpenFileCommand : BaseOpenFileCommand
    {
        public override Guid GetFileId => ((TechnicalRequrementsFileWrapper) ViewModel.SelectedItem).Id;
        public override string GetFileName => ((TechnicalRequrementsFileWrapper)ViewModel.SelectedItem).Name;
        public override string GetFilePath => GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;

        public OpenFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is TechnicalRequrementsFileWrapper;
        }
    }

}