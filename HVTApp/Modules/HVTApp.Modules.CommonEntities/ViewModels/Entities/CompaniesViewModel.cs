﻿using Prism.Commands;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using HVTApp.Services.GetEquipmentService;
using HVTApp.Services.GetProductService;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class CompaniesViewModel : BaseListViewModel<CompanyWrapper, CompanyDetailsWindowModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDialogService _dialogService;
        private readonly IUnityContainer _container;
        private readonly IGetProductService _getProductService;

        public CompaniesViewModel(IUnitOfWork unitOfWork, IDialogService dialogService, IUnityContainer container, IGetProductService getProductService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;
            _dialogService = dialogService;
            _container = container;
            _getProductService = getProductService;

            _unitOfWork.Companies.GetAll().ForEach(Items.Add);

            RefreshCommand = new DelegateCommand(RefreshCommand_Execute);
        }

        #region Commands

        public DelegateCommand RefreshCommand { get; set; }

        protected override void RemoveItemCommand_Execute()
        {
            _getProductService.GetEquipment();
        }

        protected override bool RemoveItemCommand_CanExecute()
        {
            return true;
        }

        private void RefreshCommand_Execute()
        {
            Items.Clear();
            _unitOfWork.Companies.GetAll().ForEach(Items.Add);
        }

        #endregion
    }
}