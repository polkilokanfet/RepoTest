using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model;
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
                case NotificationActionType.PriceEngineeringTaskStart:
                    return Unit.RecipientRole == Role.DesignDepartmentHead
                        ? "Назначьте исполнителя"
                        : "Проработайте задачу";

                case NotificationActionType.PriceEngineeringTaskStop:
                    return "Менеджер остановил проработку задачи";

                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                    break;
                case NotificationActionType.PriceEngineeringTaskFinish:
                    break;
                case NotificationActionType.PriceEngineeringTaskAccept:
                    break;
                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                    break;
                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                    break;
                case NotificationActionType.PriceEngineeringTaskSendMessage:
                    break;
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                    break;
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    break;
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    break;
                case NotificationActionType.PriceEngineeringTaskNotification:
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
                case NotificationActionType.SavePriceCalculation:
                    break;
                case NotificationActionType.StartPriceCalculation:
                    break;
                case NotificationActionType.CancelPriceCalculation:
                    break;
                case NotificationActionType.RejectPriceCalculation:
                    break;
                case NotificationActionType.FinishPriceCalculation:
                    break;
                case NotificationActionType.SaveTechnicalRequrementsTask:
                    break;
                case NotificationActionType.StartTechnicalRequrementsTask:
                    break;
                case NotificationActionType.InstructTechnicalRequrementsTask:
                    break;
                case NotificationActionType.RejectTechnicalRequrementsTask:
                    break;
                case NotificationActionType.RejectByFrontManagerTechnicalRequrementsTask:
                    break;
                case NotificationActionType.FinishTechnicalRequrementsTask:
                    break;
                case NotificationActionType.AcceptTechnicalRequrementsTask:
                    break;
                case NotificationActionType.StopTechnicalRequrementsTask:
                    break;
                case NotificationActionType.SaveDirectumTask:
                    break;
                case NotificationActionType.StartDirectumTask:
                    break;
                case NotificationActionType.StopDirectumTask:
                    break;
                case NotificationActionType.PerformDirectumTask:
                    break;
                case NotificationActionType.AcceptDirectumTask:
                    break;
                case NotificationActionType.RejectDirectumTask:
                    break;
                case NotificationActionType.SaveIncomingRequest:
                    break;
                case NotificationActionType.SaveActualPayment:
                    break;
                case NotificationActionType.SavePaymentDocument:
                    break;


                case NotificationActionType.PriceEngineeringTasksStart:
                case NotificationActionType.PriceEngineeringTaskStart:
                case NotificationActionType.PriceEngineeringTaskStop:
                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                case NotificationActionType.PriceEngineeringTaskFinish:
                case NotificationActionType.PriceEngineeringTaskAccept:
                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                case NotificationActionType.PriceEngineeringTaskSendMessage:
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                case NotificationActionType.PriceEngineeringTaskNotification:
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