using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class FacilityDetailsViewModel : BaseDetailsViewModel<FacilityWrapper>
    {
        public FacilityDetailsViewModel(FacilityWrapper item) : base(item)
        {
        }
    }
}