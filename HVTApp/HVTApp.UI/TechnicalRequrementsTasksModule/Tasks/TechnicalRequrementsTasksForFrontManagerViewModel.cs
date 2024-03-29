using HVTApp.Model;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTasksForFrontManagerViewModel : TechnicalRequrementsTasksBaseViewModel
    {
        public TechnicalRequrementsTasksForFrontManagerViewModel(IUnityContainer container) : base(container)
        {
        }

        protected override bool TaskIsActual(TechnicalRequrementsTask technicalRequrementsTask)
        {
            User manager = technicalRequrementsTask.FrontManager;
            return manager != null && manager.IsAppCurrentUser();
        }
    }
}