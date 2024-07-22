using System;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class OpenSpecificationScanCommand : DelegateLogCommand
    {
        private readonly Func<Specification> _getSpecification;
        private readonly Func<bool> _canExecute = null;
        private readonly IFilesStorageService _filesStorageService;
        private readonly IMessageService _messageService;
        private readonly string _storageDirectory;

        public OpenSpecificationScanCommand(Specification specification, IFilesStorageService filesStorageService, IMessageService messageService) : this(() => specification, null, filesStorageService, messageService)
        {
        }

        public OpenSpecificationScanCommand(Func<Specification> getSpecification, Func<bool> canExecute, IFilesStorageService filesStorageService, IMessageService messageService)
        {
            _getSpecification = getSpecification;
            _canExecute = canExecute;
            _filesStorageService = filesStorageService;
            _messageService = messageService;
            _storageDirectory = GlobalAppProperties.Actual.TechnicalRequrementsFilesPath;
        }

        protected override void ExecuteMethod()
        {
            var specification = this._getSpecification.Invoke();
            if (_filesStorageService.FileContainsInStorage(specification.Id, _storageDirectory) == false)
            {
                _messageService.Message("Уведомление", "Сканированная версия спецификации не загружена в хранилище");
                return;
            }

            _filesStorageService.OpenFileFromStorage(specification.Id, _storageDirectory, addToFileName:$"{specification.Contract.Number} {specification.Number}");
        }

        protected override bool CanExecuteMethod()
        {
            if (_canExecute == null) return base.CanExecuteMethod();
            return _canExecute.Invoke();
        }
    }
}