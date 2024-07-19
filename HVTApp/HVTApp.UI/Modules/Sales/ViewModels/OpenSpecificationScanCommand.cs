using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class OpenSpecificationScanCommand : DelegateLogCommand
    {
        private readonly Specification _specification;
        private readonly IFilesStorageService _filesStorageService;
        private readonly IMessageService _messageService;
        private readonly string _storageDirectory;

        public OpenSpecificationScanCommand(Specification specification, IFilesStorageService filesStorageService, IMessageService messageService)
        {
            _specification = specification;
            _filesStorageService = filesStorageService;
            _messageService = messageService;
            _storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
        }

        protected override void ExecuteMethod()
        {
            if (_filesStorageService.FileContainsInStorage(_specification.Id, _storageDirectory) == false)
            {
                _messageService.Message("Уведомление", "Сканированная версия спецификации не загружена в хранилище");
                return;
            }

            _filesStorageService.OpenFileFromStorage(_specification.Id, _storageDirectory, addToFileName:$"{_specification.Contract.Number} {_specification.Number}");
        }
    }
}