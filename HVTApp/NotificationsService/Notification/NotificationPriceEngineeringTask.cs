using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
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

        public override string GetTargetEntityInfo()
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
            sb.AppendLine($"Исполнитель: {TargetUnit.UserConstructor}");
            sb.AppendLine($"Менеджер: {tasks.UserManager}");
            sb.AppendLine($"Back-менеджер: {tasks.BackManager}");

            return sb.ToString();
        }

        public override string GetTargetActionInfo()
        {
            throw new NotImplementedException();
            switch (Unit.ActionType)
            {
                case EventServiceActionType.PriceEngineeringTaskStart:
                    return Unit.RecipientRole == Role.DesignDepartmentHead
                        ? "Назначьте исполнителя"
                        : "Проработайте задачу";

                case EventServiceActionType.PriceEngineeringTaskStop:
                    return "Менеджер остановил проработку задачи";

                case EventServiceActionType.PriceEngineeringTaskInstructToConstructor:
                    break;
                case EventServiceActionType.PriceEngineeringTaskFinish:
                    break;
                case EventServiceActionType.PriceEngineeringTaskAccept:
                    break;
                case EventServiceActionType.PriceEngineeringTaskRejectByManager:
                    break;
                case EventServiceActionType.PriceEngineeringTaskRejectByConstructorToManager:
                    break;
                case EventServiceActionType.PriceEngineeringTaskSendMessage:
                    break;
                case EventServiceActionType.PriceEngineeringTaskFinishGoToVerification:
                    break;
                case EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    break;
                case EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    break;
                case EventServiceActionType.PriceEngineeringTaskNotification:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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