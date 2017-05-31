using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TenderDetailsViewModel : BaseDetailsViewModel<TenderWrapper, Tender>
    {
        public TenderDetailsViewModel(TenderWrapper item) : base(item)
        {
        }
    }
}