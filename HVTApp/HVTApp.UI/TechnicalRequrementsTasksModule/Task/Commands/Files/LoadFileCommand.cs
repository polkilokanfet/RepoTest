using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class LoadFileCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public LoadFileCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var fileWrapper = (TechnicalRequrementsFileWrapper)ViewModel.SelectedItem;
            var storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
            string addToFileName = $"{fileWrapper.Name.ReplaceUncorrectSimbols().LimitLength()}";
            FilesStorageService.CopyFileFromStorage(fileWrapper.Id, storageDirectory, addToFileName: addToFileName);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.SelectedItem is TechnicalRequrementsFileWrapper;
        }
    }
}