using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.View;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationPriceEngineeringTask : Notification<PriceEngineeringTask>
    {
        public NotificationPriceEngineeringTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager) : base(unitOfWork, unit, regionManager)
        {
        }

        public override string GetCommonInfo()
        {
            var tasks = TargetUnit.GetPriceEngineeringTasks(UnitOfWork);
            var taskTop = TargetUnit.GetTopPriceEngineeringTask(UnitOfWork);
            var salesUnit = taskTop.SalesUnits.FirstOrDefault();

            var sb = new StringBuilder();
            sb.AppendLine($"Номер сборки в УП ВВА: {tasks.NumberFull}");
            sb.AppendLine($"Номер задачи в УП ВВА: {TargetUnit.Number}");
            sb.AppendLine($"Номер задачи в Team Center: {tasks.TceNumber}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"Проект: {salesUnit?.Project}");
            sb.AppendLine($"Объект: {salesUnit?.Facility}");
            sb.AppendLine($"Оборудование: {taskTop.ProductBlock};");
            sb.AppendLine($"Блок оборудования: {TargetUnit.ProductBlock}");
            sb.AppendLine(string.Empty);

            sb.AppendLine($"Бюро ОГК: {TargetUnit.DesignDepartment}");
            sb.AppendLine($"Исполнитель (от ОГК): {TargetUnit.UserConstructor}");
            sb.AppendLine($"Менеджер: {tasks.UserManager}");
            sb.AppendLine($"Back-менеджер: {tasks.BackManager}");

            return sb.ToString();
        }

        public override string GetActionInfo()
        {
            switch (Unit.ActionType)
            {
                case NotificationActionType.PriceEngineeringTaskStart:
                    return Unit.RecipientRole == Role.DesignDepartmentHead
                        ? "Назначьте исполнителя"
                        : "Проработайте задачу";

                case NotificationActionType.PriceEngineeringTaskStop:
                    return "Менеджер остановил проработку блока ТСП";

                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                    break;
                case NotificationActionType.PriceEngineeringTaskFinish:
                    break;
                case NotificationActionType.PriceEngineeringTaskAccept:
                    break;
                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                    break;

                case NotificationActionType.PriceEngineeringTaskRejectByHeadToManager:
                    return "Проработка блока ТСП отклонена руководителем КБ";

                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                    return "Проработка блока ТСП отклонена исполнителем";

                case NotificationActionType.PriceEngineeringTaskSendMessage:
                    break;
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                    break;
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    break;
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new NotImplementedException();
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };

            switch (this.Unit.RecipientRole)
            {
                case Role.SalesManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(parameters);

                case Role.Constructor:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewConstructor>(parameters);

                case Role.BackManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);

                case Role.BackManagerBoss:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);

                case Role.DesignDepartmentHead:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(parameters);

                case Role.PlanMaker:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewPlanMaker>(parameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }
}