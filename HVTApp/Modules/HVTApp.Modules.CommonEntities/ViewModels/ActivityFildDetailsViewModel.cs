using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField>
    {
        public ActivityFildDetailsViewModel(ActivityFieldWrapper item) : base(item)
        {
        }
    }
}