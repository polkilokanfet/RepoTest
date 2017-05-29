using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField>
    {
        public ActivityFildDetailsViewModel(ActivityFieldWrapper item) : base(item)
        {
        }
    }
}