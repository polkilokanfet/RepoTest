using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Events.NotificationArgs
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

        public string Message => GetMessageSimple();

        protected NotificationAboutPriceEngineeringTaskEventArg(PriceEngineeringTask priceEngineeringTask, User recipientUser, Role recipientRole)
        {
            PriceEngineeringTask = priceEngineeringTask;
            RecipientUser = recipientUser;
            RecipientRole = recipientRole;
        }

        protected abstract string GetMessageSimple();

        public class VerificationAcceptByHeadToManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public VerificationAcceptByHeadToManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП проработано";
            }
        }

        public class VerificationAcceptByHeadToConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public VerificationAcceptByHeadToConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП проверено руководителем";
            }
        }

        public class AcceptedByManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public AcceptedByManager(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП принята менеджером";
            }
        }

        public class FinishByConstructorToDesignDepartmentHead : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToDesignDepartmentHead(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.DesignDepartment.Head, Role.DesignDepartmentHead)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Проверьте ТСП";
            }
        }

        public class FinishByConstructorToManager1 : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToManager1(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП на проверке";
            }
        }

        public class FinishByConstructorToManager2 : NotificationAboutPriceEngineeringTaskEventArg
        {
            public FinishByConstructorToManager2(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП проработано";
            }
        }

        public class LoadToTceFinish : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceFinish(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП загружено в TeamCenter";
            }
        }

        public class LoadToTceStartBackManagerBoss : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceStartBackManagerBoss(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Поручите загрузку в Тeam Сenter";
            }
        }

        public class LoadToTceStartBackManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public LoadToTceStartBackManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Загрузите в Тeam Сenter";
            }
        }

        public class ProductionRequestFinish : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestFinish(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Производство открыто";
            }
        }

        public class ProductionRequestStartBackManagerBoss : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestStartBackManagerBoss(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Назначьте плановика";
            }
        }

        public class ProductionRequestStartPlanMaker : NotificationAboutPriceEngineeringTaskEventArg
        {
            public ProductionRequestStartPlanMaker(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.PlanMaker)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Откройте производство по ТСП";
            }
        }

        public class RejectByHeadToConstructorManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToConstructorManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП требует доработки";
            }
        }

        public class RejectByHeadToConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП требует доработки";
            }
        }

        public class RejectByHeadToManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectByHeadToManager(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Руководитель КБ отклонил ТСП";
            }
        }

        public class RejectedByConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectedByConstructor(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Исполнитель отклонил ТСП";
            }
        }

        public class RejectedByManager : NotificationAboutPriceEngineeringTaskEventArg
        {
            public RejectedByManager(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Проработка ТСП отклонена менеджером";
            }
        }

        public class StartDesignDepartmentHead : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StartDesignDepartmentHead(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.DesignDepartment.Head, Role.DesignDepartmentHead)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Поручите ТСП";
            }
        }

        public class StartConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StartConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Проработайте ТСП";
            }
        }

        public class StopConstructor : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopConstructor(PriceEngineeringTask priceEngineeringTask) : base(priceEngineeringTask, priceEngineeringTask.UserConstructor, Role.Constructor)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"ТСП остановлена";
            }
        }

        public class StopProductionRequest : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequest(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.BackManagerBoss)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Запрошена остановка производства";
            }
        }

        public class StopProductionRequestConfirm : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequestConfirm(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Производство остановлено";
            }
        }

        public class StopProductionRequestReject : NotificationAboutPriceEngineeringTaskEventArg
        {
            public StopProductionRequestReject(PriceEngineeringTask priceEngineeringTask, User recipientUser) : base(priceEngineeringTask, recipientUser, Role.SalesManager)
            {
            }

            protected override string GetMessageSimple()
            {
                return $"Запрос на остановку производства отклонен";
            }
        }
    }
}