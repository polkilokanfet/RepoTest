using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace NotificationsFromDataBaseService
{
    public class NotificationFromDataBaseService : INotificationFromDataBaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationGeneratorService _notificationGeneratorService;
        private readonly IPopupNotificationsService _popupNotificationsService;

        public NotificationFromDataBaseService(IUnitOfWork unitOfWork, IPopupNotificationsService popupNotificationsService, INotificationGeneratorService notificationGeneratorService)
        {
            _unitOfWork = unitOfWork;
            _popupNotificationsService = popupNotificationsService;
            _notificationGeneratorService = notificationGeneratorService;
        }

        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        public void SaveNotificationInDataBase(NotificationUnit unit)
        {
            var senderUserId = unit.SenderUser?.Id ?? unit.SenderUserId;
            unit.SenderUser = _unitOfWork.Repository<User>().GetById(senderUserId);

            var recipientUserId = unit.RecipientUser?.Id ?? unit.RecipientUserId;
            unit.RecipientUser = _unitOfWork.Repository<User>().GetById(recipientUserId);

            _unitOfWork.SaveEntity(unit);
        }

        public void RemoveNotificationFromDataBase(NotificationUnit unit)
        {
            var notificationUnit = _unitOfWork.Repository<NotificationUnit>().GetById(unit.Id);
            if (notificationUnit == null) return;
            _unitOfWork.RemoveEntity(notificationUnit);
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

        public void CheckMessagesInDbAndShowNotifications()
        {
            //Есть ли в базе данных сообщения для текущего пользователя в текущей роли?
            var notificationUnits = _unitOfWork.Repository<NotificationUnit>()
                .Find(unit => unit.RecipientUser.Id == GlobalAppProperties.User.Id &&
                              unit.RecipientRole == GlobalAppProperties.User.RoleCurrent);

            foreach (var notificationUnit in notificationUnits)
            {
                //показ уведомления
                this.ShowNotification(notificationUnit);

                //удаление уведомления
                _unitOfWork.Repository<NotificationUnit>().Delete(notificationUnit);
            }

            _unitOfWork.SaveChanges();
        }
    }
}
