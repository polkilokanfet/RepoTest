using System;
using EventServiceClient2.SyncEntities;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace EventServiceClient2.ServiceCallbackBase
{
    public class ServiceCallbackBaseTechnicalRequarementsTask<TAfterTechnicalRequrementsTaskEvent>
        where TAfterTechnicalRequrementsTaskEvent : PubSubEvent<TechnicalRequrementsTask>
    {
        private readonly IUnityContainer _container;
        private readonly SyncContainer _syncContainer;

        public ServiceCallbackBaseTechnicalRequarementsTask(IUnityContainer container, SyncContainer syncContainer)
        {
            _container = container;
            _syncContainer = syncContainer;
        }

        public void Start(TechnicalRequrementsTask technicalRequrementsTask, string message)
        {
            var action = new Action(() =>
            {
                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(TechnicalRequrementsTask), technicalRequrementsTask } });
            });

            _syncContainer.Publish<TechnicalRequrementsTask, TAfterTechnicalRequrementsTaskEvent>(technicalRequrementsTask);
            Popup.Popup.ShowPopup(message, $"Задача в TCE с Id {technicalRequrementsTask.Id}", action);
        }
    }
}