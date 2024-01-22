using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.EventServiceEvents.Args
{
    /// <summary>
    /// Единица уведомления
    /// </summary>
    public abstract class NotificationAboutPriceEngineeringTaskEventArg
    {
        /// <summary>
        /// Связанная с уведомлением задача ТСП
        /// </summary>
        public PriceEngineeringTask PriceEngineeringTask { get; }

        #region Sender

        /// <summary>
        /// отправитель
        /// </summary>
        public User SenderUser { get; set; } = GlobalAppProperties.User;

        /// <summary>
        /// роль отправителя
        /// </summary>
        public Role SenderRole { get; set; } = GlobalAppProperties.User.RoleCurrent;

        #endregion

        #region Recipient

        /// <summary>
        /// Получатель
        /// </summary>
        public User RecipientUser { get; }

        /// <summary>
        /// Роль получателя
        /// </summary>
        public Role RecipientRole { get; }

        #endregion

        protected NotificationAboutPriceEngineeringTaskEventArg(PriceEngineeringTask priceEngineeringTask, User recipientUser, Role recipientRole)
        {
            PriceEngineeringTask = priceEngineeringTask;
            RecipientUser = recipientUser;
            RecipientRole = recipientRole;
        }

        public abstract string GetMessageSimple();

        public string GetMessageEmail()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Номер задачи: {PriceEngineeringTask.Number}");
            sb.AppendLine(this.GetMessageEmail2());
            return sb.ToString();
        }

        protected virtual string GetMessageEmail2()
        {
            return GetMessageSimple();
        }

        public class VerificationAcceptByHeadToManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public VerificationAcceptByHeadToManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП проработано: {this.PriceEngineeringTask}";
            }
        }

        public class VerificationAcceptByHeadToConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public VerificationAcceptByHeadToConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП проверено руководителем: {this.PriceEngineeringTask}";
            }
        }

        public class AcceptedByManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public AcceptedByManager(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП принята менеджером: {PriceEngineeringTask}";
            }
        }

        public class FinishByConstructorToDesignDepartmentHead : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToDesignDepartmentHead(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.DesignDepartment.Head, Role.DesignDepartmentHead)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Проверьте ТСП: {PriceEngineeringTask}";
            }
        }

        public class FinishByConstructorToManager1 : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToManager1(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП на проверке: {PriceEngineeringTask}";
            }
        }

        public class FinishByConstructorToManager2 : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToManager2(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП проработано: {PriceEngineeringTask}";
            }
        }

        public class LoadToTceFinish : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceFinish(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП загружено в TeamCenter: {PriceEngineeringTask}";
            }
        }

        public class LoadToTceStartBackManagerBoss : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceStartBackManagerBoss(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Поручите загрузку в Тeam Сenter: {PriceEngineeringTask}";
            }
        }

        public class LoadToTceStartBackManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceStartBackManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Загрузите в Тeam Сenter: {PriceEngineeringTask}";
            }
        }

        public class ProductionRequestFinish : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestFinish(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Производство открыто: {PriceEngineeringTask}";
            }
        }

        public class ProductionRequestStartBackManagerBoss : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestStartBackManagerBoss(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Назначьте плановика: {PriceEngineeringTask}";
            }
        }

        public class ProductionRequestStartPlanMaker : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestStartPlanMaker(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.PlanMaker)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Откройте производство по ТСП: {PriceEngineeringTask}";
            }
        }

        public class RejectByHeadToConstructorManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToConstructorManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП требует доработки: {PriceEngineeringTask}";
            }
        }

        public class RejectByHeadToConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП требует доработки: {PriceEngineeringTask}";
            }
        }

        public class RejectByHeadToManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Руководитель КБ отклонил ТСП: {PriceEngineeringTask}";
            }
        }

        public class RejectedByConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectedByConstructor(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Исполнитель отклонил ТСП: {PriceEngineeringTask}";
            }
        }

        public class RejectedByManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectedByManager(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Проработка ТСП отклонена менеджером: {PriceEngineeringTask}";
            }
        }

        public class StartDesignDepartmentHead : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StartDesignDepartmentHead(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.DesignDepartment.Head, Role.DesignDepartmentHead)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Поручите ТСП: {PriceEngineeringTask}";
            }
        }

        public class StartConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StartConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Проработайте ТСП: {PriceEngineeringTask}";
            }
        }

        public class StopConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            public override string GetMessageSimple()
            {
                return $"ТСП остановлена: {PriceEngineeringTask}";
            }
        }

        public class StopProductionRequest : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequest(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Запрошена остановка производства: {PriceEngineeringTask}";
            }
        }

        public class StopProductionRequestConfirm : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequestConfirm(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Производство остановлено: {PriceEngineeringTask}";
            }
        }

        public class StopProductionRequestReject : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequestReject(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            public override string GetMessageSimple()
            {
                return $"Запрос на остановку производства отклонен: {PriceEngineeringTask}";
            }
        }
    }
}