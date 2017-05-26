using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    class ContractsViewModel : EditableBase<ContractWrapper, ContractDetailsViewModel, Contract>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;

            
        }
    }
}
