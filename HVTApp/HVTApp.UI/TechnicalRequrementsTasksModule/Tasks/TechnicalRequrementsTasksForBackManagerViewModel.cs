using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTasksForBackManagerViewModel : TechnicalRequrementsTasksBaseViewModel
    {
        public TechnicalRequrementsTasksForBackManagerViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(TechnicalRequrementsTask technicalRequrementsTask)
        {
            User manager = technicalRequrementsTask.BackManager;
            return manager != null && manager.IsAppCurrentUser();
        }
    }
}