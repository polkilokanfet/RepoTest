using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationPriceCalculation : Notification<PriceCalculation>
    {
        public NotificationPriceCalculation(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager) : base(unitOfWork, unit, regionManager)
        {
        }

        public override string GetTargetEntityInfo()
        {
            return this.TargetUnit.ToString();
        }

        public override string GetTargetActionInfo()
        {
            switch (Unit.ActionType)
            {
                case NotificationActionType.SavePriceCalculation:
                    return "Сохранен расчёт переменных затрат";
                case NotificationActionType.StartPriceCalculation:
                    return "Запущен расчёт переменных затрат";
                case NotificationActionType.CancelPriceCalculation:
                    return "Остановлен расчёт переменных затрат";
                case NotificationActionType.RejectPriceCalculation:
                    return "Отклонен расчёт переменных затрат";
                case NotificationActionType.FinishPriceCalculation:
                    return "Завершен расчёт переменных затрат";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };
            return () => RegionManager.RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), parameters } });
        }
    }
}