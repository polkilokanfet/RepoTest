using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    class ContractsViewModel : BaseListViewModel<ContractWrapper, ContractDetailsViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(unitOfWork, container, dialogService)
        {
            _unitOfWork = unitOfWork;

            
        }
    }
}
