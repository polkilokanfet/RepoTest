using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculations.Commands
{
    public class LoadFileCommand : DelegateLogCommand
    {
        private readonly PriceCalculationsViewModel _viewModel;
        private readonly IUnityContainer _container;
        private readonly IFilesStorageService _filesStorageService;

        public LoadFileCommand(PriceCalculationsViewModel viewModel, IUnityContainer container)
        {
            _viewModel = viewModel;
            _container = container;
            _filesStorageService = container.Resolve<IFilesStorageService>();
        }

        protected override void ExecuteMethod()
        {
            var messageService = _container.Resolve<IMessageService>();
            if (!_viewModel.SelectedItem.Files.Any())
            {
                messageService.ShowOkMessageDialog("Информация", "В этот расчет ещё не загружен ни один файл.");
                return;
            }

            var file = _viewModel.SelectedItem.Files.First();
            if (_viewModel.SelectedItem.Files.Count > 1)
            {
                var selectService = _container.Resolve<ISelectService>();
                file = selectService.SelectItem(_viewModel.SelectedItem.Files);
                if (file == null)
                    return;
            }

            var storageDirectory = GlobalAppProperties.Actual.PriceCalculationsFilesPath;
            string addToFileName = $"{file.CreationMoment.ToShortDateString()} {file.CreationMoment.ToShortTimeString()}";
            _filesStorageService.CopyFileFromStorage(file.Id, storageDirectory, addToFileName: addToFileName.ReplaceUncorrectSimbols("-"));
        }

        protected override bool CanExecuteMethod()
        {
            return _viewModel.SelectedLookup != null;
        }
    }
}