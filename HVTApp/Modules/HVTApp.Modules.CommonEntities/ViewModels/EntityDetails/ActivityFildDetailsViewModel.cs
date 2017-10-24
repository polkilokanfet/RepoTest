using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ActivityFildDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField>
    {
        public ActivityFildDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}