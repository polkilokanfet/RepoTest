using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper>
    {
        public ActivityFildDetailsViewModel(ActivityFieldWrapper item) : base(item)
        {
        }
    }
}