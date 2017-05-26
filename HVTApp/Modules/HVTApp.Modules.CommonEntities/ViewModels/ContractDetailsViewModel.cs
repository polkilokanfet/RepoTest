using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    internal class ContractDetailsViewModel : BaseDetailsViewModel<ContractWrapper, Contract>
    {
        public ContractDetailsViewModel(ContractWrapper item) : base(item)
        {
        }
    }
}