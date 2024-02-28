using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INotificationGeneratorService
    {
        /// <summary>
        /// Общая информация
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        string GetCommonInfo(NotificationUnit unit);

        /// <summary>
        /// Информация о требуемом действии
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        string GetActionInfo(NotificationUnit unit);

        Action GetOpenTargetEntityViewAction(NotificationUnit unit);

        void RefreshTargetEntityAction(NotificationUnit unit);
    }
}