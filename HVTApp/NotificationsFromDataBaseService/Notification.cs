using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;

namespace NotificationsFromDataBaseService
{
    internal interface INotification
    {
        string GetTargetEntityInfo();
        string GetTargetActionInfo();
    }

    internal abstract class Notification<TTargetEntity> : INotification where  TTargetEntity : class, IBaseEntity
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected NotificationUnit Unit { get; }

        public TTargetEntity TargetUnit => UnitOfWork.Repository<TTargetEntity>().GetById(Unit.TargetEntityId);

        protected Notification(IUnitOfWork unitOfWork, NotificationUnit unit)
        {
            UnitOfWork = unitOfWork;
            Unit = unit;
        }

        public abstract string GetTargetEntityInfo();
        public abstract string GetTargetActionInfo();
    }

    internal class NotificationPriceEngineeringTask : Notification<PriceEngineeringTask>
    {
        public NotificationPriceEngineeringTask(IUnitOfWork unitOfWork, NotificationUnit unit) : base(unitOfWork, unit)
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

                case EventServiceActionType.PriceEngineeringTaskInstruct:
                    break;
                case EventServiceActionType.PriceEngineeringTaskFinish:
                    break;
                case EventServiceActionType.PriceEngineeringTaskAccept:
                    break;
                case EventServiceActionType.PriceEngineeringTaskRejectByManager:
                    break;
                case EventServiceActionType.PriceEngineeringTaskRejectByConstructor:
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
    }

    public class NotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private INotification GetNotification(NotificationUnit unit)
        {
            throw new NotImplementedException();
            switch (unit.ActionType)
            {
                case EventServiceActionType.SavePriceCalculation:
                    break;
                case EventServiceActionType.StartPriceCalculation:
                    break;
                case EventServiceActionType.CancelPriceCalculation:
                    break;
                case EventServiceActionType.RejectPriceCalculation:
                    break;
                case EventServiceActionType.FinishPriceCalculation:
                    break;
                case EventServiceActionType.SaveTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.StartTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.InstructTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.RejectTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.RejectByFrontManagerTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.FinishTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.AcceptTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.StopTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.SaveDirectumTask:
                    break;
                case EventServiceActionType.StartDirectumTask:
                    break;
                case EventServiceActionType.StopDirectumTask:
                    break;
                case EventServiceActionType.PerformDirectumTask:
                    break;
                case EventServiceActionType.AcceptDirectumTask:
                    break;
                case EventServiceActionType.RejectDirectumTask:
                    break;
                case EventServiceActionType.SaveIncomingRequest:
                    break;
                case EventServiceActionType.SaveActualPayment:
                    break;
                case EventServiceActionType.SavePaymentDocument:
                    break;


                case EventServiceActionType.PriceEngineeringTasksStart:
                case EventServiceActionType.PriceEngineeringTaskStart:
                case EventServiceActionType.PriceEngineeringTaskStop:
                case EventServiceActionType.PriceEngineeringTaskInstruct:
                case EventServiceActionType.PriceEngineeringTaskFinish:
                case EventServiceActionType.PriceEngineeringTaskAccept:
                case EventServiceActionType.PriceEngineeringTaskRejectByManager:
                case EventServiceActionType.PriceEngineeringTaskRejectByConstructor:
                case EventServiceActionType.PriceEngineeringTaskSendMessage:
                case EventServiceActionType.PriceEngineeringTaskFinishGoToVerification:
                case EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                case EventServiceActionType.PriceEngineeringTaskNotification:
                    return new NotificationPriceEngineeringTask(_unitOfWork, unit);


                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public string GetTargetEntityInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetTargetEntityInfo();
        }

        public string GetTargetActionInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetTargetActionInfo();
        }

    }

}