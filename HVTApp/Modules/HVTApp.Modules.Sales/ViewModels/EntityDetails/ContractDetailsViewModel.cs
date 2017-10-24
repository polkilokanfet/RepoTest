using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    internal class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper>
    {
        public ContractDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}