using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class LoadSpecificationScanCommand : DelegateLogCommand
    {
        private readonly Specification _specification;
        private readonly IFilesStorageService _filesStorageService;
        private readonly IMessageService _messageService;
        private readonly string _storageDirectory;

        public LoadSpecificationScanCommand(Specification specification, IFilesStorageService filesStorageService, IMessageService messageService)
        {
            _specification = specification;
            _filesStorageService = filesStorageService;
            _messageService = messageService;
            _storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
        }

        protected override void ExecuteMethod()
        {
            if (_filesStorageService.FileContainsInStorage(_specification.Id, _storageDirectory))
            {
                var dr = _messageService.ConfirmationDialog("Данная спецификация уже загружена в хранилище. Заменить?");
                if (dr == false) return;
            }

            _filesStorageService.LoadFileToStorage(_storageDirectory, _specification.Id, true);
        }
    }
}