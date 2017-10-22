using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.Sales.ViewModels
{
    internal class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper>
    {
        public ContractDetailsViewModel(ContractWrapper item) : base(item)
        {
        }
    }
}