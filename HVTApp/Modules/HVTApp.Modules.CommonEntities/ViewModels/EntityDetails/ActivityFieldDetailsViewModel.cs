using HVTApp.Model.POCOs;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ActivityFieldDetailsViewModel : BaseDetailsViewModel<ActivityFieldWrapper, ActivityField, AfterSaveActivityFieldEvent>
    {
        public ActivityFieldDetailsViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}