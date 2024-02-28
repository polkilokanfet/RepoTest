using System;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceEngineeringTasks : NotificationHelper<PriceEngineeringTasks, AfterSavePriceEngineeringTasksEvent>
    {
        public NotificationHelperPriceEngineeringTasks(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator) : 
            base(unitOfWork, unit, regionManager, eventAggregator)
        {
        }

        public override string GetCommonInfo()
        {
            var tasks = TargetUnit;

            var sb = new StringBuilder();
            sb.AppendLine($"Номер сборки в УП ВВА: {tasks.NumberFull}");
            sb.AppendLine($"Номер задачи в УП ВВА: {TargetUnit.Number}");
            sb.AppendLine($"Номер задачи в Team Center: {tasks.TceNumber}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"Менеджер: {tasks.UserManager}");
            sb.AppendLine($"Back-менеджер: {tasks.BackManager}");

            return sb.ToString();
        }

        public override string GetActionInfo()
        {
            switch (Unit.ActionType)
            {
                case NotificationActionType.PriceEngineeringTasksStart:
                    return "Запущена задача ТСП";

                case NotificationActionType.PriceEngineeringTasksInstructToBackManager:
                    return Unit.RecipientRole == Role.BackManager
                        ? "Загрузите результаты проработки ТСП в Team Center"
                        : "Назначен Бэкменеджер";

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };

            switch (this.Unit.RecipientRole)
            {
                case Role.BackManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);

                case Role.BackManagerBoss:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}