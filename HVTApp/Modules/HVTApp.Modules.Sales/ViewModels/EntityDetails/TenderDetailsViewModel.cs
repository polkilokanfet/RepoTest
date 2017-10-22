using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper>
    {
        public TenderDetailsViewModel(TenderWrapper item) : base(item)
        {
        }
    }
}