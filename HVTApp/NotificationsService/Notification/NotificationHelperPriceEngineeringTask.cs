using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceEngineeringTask : NotificationHelper<PriceEngineeringTask, AfterSavePriceEngineeringTaskEvent>
    {
        public NotificationHelperPriceEngineeringTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator) : 
            base(unitOfWork, unit, regionManager,eventAggregator)
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
                    return Unit.RecipientRole == Role.Constructor
                        ? "Проработайте блок"
                        : "Назначен исполнитель";

                case NotificationActionType.PriceEngineeringTaskFinish:
                    return "Блок ТСП проработан";

                case NotificationActionType.PriceEngineeringTaskAccept:
                    return "Проработка блока ТСП принята менеджером";

                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                    return "Проработка блока ТСП отклонена менеджером (необходима доработка)";

                case NotificationActionType.PriceEngineeringTaskRejectByHeadToManager:
                    return "Проработка блока ТСП отклонена руководителем КБ";

                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                    return "Проработка блока ТСП отклонена исполнителем";

                case NotificationActionType.PriceEngineeringTaskSendMessage:
                    return "Новое сообщение в проработке ТСП";

                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                    return Unit.RecipientRole == Role.DesignDepartmentHead
                        ? "Проверьте проработку"
                        : "Блок ТСП отправлен на проверку руководителю КБ";

                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    return Unit.RecipientRole == Role.Constructor
                        ? "Проработка не согласована руководителем КБ (перепроработайте)"
                        : "Проработка не согласована руководителем КБ";

                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    return "Проработка согласована руководителем КБ";

                case NotificationActionType.PriceEngineeringTaskInstructToPlanMaker:
                    return Unit.RecipientRole == Role.PlanMaker
                        ? "Откройте производство"
                        : "Назначен плановик";

                case NotificationActionType.PriceEngineeringTaskLoadToTceStart:
                    return Unit.RecipientRole == Role.BackManager
                        ? "Загрузите результаты проработки ТСП в Team Center"
                        : "Назначьте Бэкменеджера";

                case NotificationActionType.PriceEngineeringTaskLoadToTceFinish:
                    return "Проработка загружена в Team Center";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStart:
                    return Unit.RecipientRole == Role.BackManagerBoss
                        ? "Назначте плановика (для открытия производства)"
                        : "Отправлен запрос на открытие производства";

                case NotificationActionType.PriceEngineeringTaskProductionRequestFinish:
                    return "Производство открыто";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStop:
                    return "Заявка на остановку производства";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStopConfirm:
                    return "Заявка на остановку производства согласована";

                case NotificationActionType.PriceEngineeringTaskProductionRequestStopReject:
                    return "Заявка на остановку производства отклонена";

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