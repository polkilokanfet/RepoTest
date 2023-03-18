using System;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2
{
    public partial class EventServiceClient
    {
        public bool OnPriceEngineeringNotificationServiceCallback(Guid priceEngineeringTaskId, string message)
        {
            var priceEngineeringTask = _container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }
    }
}