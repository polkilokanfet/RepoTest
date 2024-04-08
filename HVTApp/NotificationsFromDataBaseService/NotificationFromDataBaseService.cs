using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace NotificationsFromDataBaseService
{
    public class NotificationFromDataBaseService : INotificationFromDataBaseService
    {
        private readonly IUnityContainer _container;
        private readonly INotificationGeneratorService _notificationGeneratorService;
        private readonly IPopupNotificationsService _popupNotificationsService;

        public NotificationFromDataBaseService(IUnityContainer container, IPopupNotificationsService popupNotificationsService, INotificationGeneratorService notificationGeneratorService)
        {
            _container = container;
            _popupNotificationsService = popupNotificationsService;
            _notificationGeneratorService = notificationGeneratorService;
        }

        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        public void SaveNotificationInDataBase(NotificationUnit unit)
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var senderUserId = unit.SenderUser?.Id ?? unit.SenderUserId;
                unit.SenderUser = unitOfWork.Repository<User>().GetById(senderUserId);

                var recipientUserId = unit.RecipientUser?.Id ?? unit.RecipientUserId;
                unit.RecipientUser = unitOfWork.Repository<User>().GetById(recipientUserId);

                unitOfWork.SaveEntity(unit);
            }
        }

        public void RemoveNotificationFromDataBase(NotificationUnit unit)
        {
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                var notificationUnit = unitOfWork.Repository<NotificationUnit>().GetById(unit.Id);
                if (notificationUnit == null) return;
                unitOfWork.RemoveEntity(notificationUnit);
            }
        }

        public void ShowNotification(NotificationUnit notificationUnit)
        {
            var title = _notificationGeneratorService.GetActionInfo(notificationUnit);
            var message = _notificationGeneratorService.GetCommonInfo(notificationUnit);
            var action = _notificationGeneratorService.GetOpenTargetEntityViewAction(notificationUnit);
            _popupNotificationsService.ShowNotification(message, title, action);
            
            //обновление измененной сущности
            _notificationGeneratorService.RefreshTargetEntityAction(notificationUnit);
        }

        private bool _checkingProcessInProgress = false;
        public void CheckMessagesInDbAndShowNotifications()
        {
            if (_checkingProcessInProgress == true) return;

            _checkingProcessInProgress = true;
            using (var unitOfWork = _container.Resolve<IUnitOfWork>())
            {
                //Есть ли в базе данных сообщения для текущего пользователя в текущей роли?
                var notificationUnits = unitOfWork.Repository<NotificationUnit>()
                    .Find(unit => unit.RecipientUser.Id == GlobalAppProperties.User.Id &&
                                  unit.RecipientRole == GlobalAppProperties.User.RoleCurrent);

                foreach (var notificationUnit in notificationUnits)
                {
                    //показ уведомления
                    this.ShowNotification(notificationUnit);
                    //удаление уведомления
                    unitOfWork.RemoveEntity(notificationUnit);
                }
            }

            _checkingProcessInProgress = false;
        }
    }
}
