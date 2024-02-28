using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceCalculation : NotificationHelper<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public NotificationHelperPriceCalculation(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator) : 
            base(unitOfWork, unit, regionManager, eventAggregator)
        {
        }

        public override string GetCommonInfo()
        {
            var priceCalculation = this.TargetUnit;
            var sb = new StringBuilder();
            sb.AppendLine("Расчёт переменных затрат");
            sb.AppendLine("Оборудование:");
            foreach (var priceCalculationItem in priceCalculation.PriceCalculationItems.Where(x => x.SalesUnits.Any()))
            {
                var salesUnit = priceCalculationItem.SalesUnits.First();
                sb.AppendLine($" - Объект: {salesUnit.Facility}; Оборудование: {salesUnit.Product}; Количество: {priceCalculationItem.SalesUnits.Count}");
            }
            return sb.ToString();
        }

        public override string GetActionInfo()
        {
            switch (Unit.ActionType)
            {
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
            return () => RegionManager.RequestNavigateContentRegion<PriceCalculationView>(parameters);
        }
    }
}