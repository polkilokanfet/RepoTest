using System.Linq;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    class ContractsViewModel : BaseListViewModel<ContractWrapper, ContractDetailsViewModel>
    {
        public ContractsViewModel(IUnitOfWork unitOfWork, IUnityContainer container, IDialogService dialogService) : base(container)
        {
            unitOfWork.Contracts.GetAll().Select(x => new ContractWrapper(x)).ForEach(Items.Add);
        }
    }
}
