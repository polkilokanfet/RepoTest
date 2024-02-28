using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class TechnicalRequrementsTasksForBackManagerBossViewModel : TechnicalRequrementsTasksBaseViewModel
    {
        public TechnicalRequrementsTasksForBackManagerBossViewModel(IUnityContainer container) : base(container)
        {
            //подписка на событие старта задачи в ТСЕ (событие прилетает из сервиса синхронизации)
            EventAggregator.GetEvent<AfterSaveTechnicalRequrementsTaskEvent>().Subscribe(OnAfterSaveEntity, true);
        }

        protected override bool TaskIsActual(TechnicalRequrementsTask technicalRequrementsTask)
        {
            return true;
        }
    }
}