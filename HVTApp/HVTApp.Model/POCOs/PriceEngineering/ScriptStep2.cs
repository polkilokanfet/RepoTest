using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using Prism.Events;

namespace HVTApp.Model.POCOs
{
    public abstract class ScriptStep2 : SmartEnumeration<ScriptStep2>
    {
        protected readonly Role Role;

        public abstract string FullName { get; }

        /// <summary>
        /// С каких этапов можно перейти на данный (в текущей роли).
        /// </summary>
        public IEnumerable<ScriptStep2> PossiblePreviousSteps { get; } = new List<ScriptStep2>();

        /// <summary>
        /// На какие этапы можно перейти с данного (в текущей роли).
        /// </summary>
        public IEnumerable<ScriptStep2> PossibleNextSteps
        {
            get
            {
                return ScriptStep2
                    .GetMembers()
                    .Where(step => step.Equals(this) == false)
                    .Where(x => x.AllowDoStep(this));
            }
        }

        #region Steps

        /// <summary>
        /// Задача создана
        /// </summary>
        public static readonly ScriptStep2 Created = new CreatedStep();

        /// <summary>
        /// Задача запущена на проработку
        /// </summary>
        public static readonly ScriptStep2 Started = new StartedStep();

        /// <summary>
        /// Задача остановлена менеджером
        /// </summary>
        public static readonly ScriptStep2 Stopped = new StoppedStep();

        /// <summary>
        /// Проработка отклонена менеджером конструктору
        /// </summary>
        public static readonly ScriptStep2 RejectedByManager = new RejectedByManagerStep();

        /// <summary>
        /// Проработка отклонена конструктором менеджеру
        /// </summary>
        public static readonly ScriptStep2 RejectedByConstructor = new RejectedByConstructorStep();

        /// <summary>
        /// Проработка завершена конструктором
        /// </summary>
        public static readonly ScriptStep2 FinishedByConstructor = new FinishedByConstructorStep();

        /// <summary>
        /// Проработка задачи принята менеджером
        /// </summary>
        public static readonly ScriptStep2 Accepted = new AcceptedStep();

        /// <summary>
        /// Конструктор направил проработку руководителю на проверку
        /// </summary>
        public static readonly ScriptStep2 VerificationRequestedByConstructor = new VerificationRequestedByConstructorStep();

        /// <summary>
        /// Руководитель согласовал проработку конструктору
        /// </summary>
        public static readonly ScriptStep2 VerificationAcceptedByHead = new VerificationAcceptedByHeadStep();

        /// <summary>
        /// Руководитель отклонил проработку конструктору
        /// </summary>
        public static readonly ScriptStep2 VerificationRejectedByHead = new VerificationRejectedByHeadStep();

        #endregion

        #region ctors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="role">Роль, в которой пожно перейти на данный этап</param>
        private ScriptStep2(int value, Role role) : base(value)
        {
            Role = role;
        }

        static ScriptStep2()
        {
            Started
                .AddPossiblePreviousStep(Created)
                .AddPossiblePreviousStep(RejectedByConstructor);

            Stopped
                .AddPossiblePreviousStep(Started)
                .AddPossiblePreviousStep(RejectedByManager)
                .AddPossiblePreviousStep(RejectedByConstructor)
                .AddPossiblePreviousStep(FinishedByConstructor)
                .AddPossiblePreviousStep(VerificationRequestedByConstructor)
                .AddPossiblePreviousStep(VerificationAcceptedByHead)
                .AddPossiblePreviousStep(VerificationRejectedByHead);

            RejectedByManager
                .AddPossiblePreviousStep(FinishedByConstructor);

            RejectedByConstructor
                .AddPossiblePreviousStep(Started);

            FinishedByConstructor
                .AddPossiblePreviousStep(Started)
                .AddPossiblePreviousStep(VerificationAcceptedByHead);

            Accepted
                .AddPossiblePreviousStep(FinishedByConstructor);

            VerificationRequestedByConstructor
                .AddPossiblePreviousStep(Started)
                .AddPossiblePreviousStep(VerificationRejectedByHead);

            VerificationAcceptedByHead
                .AddPossiblePreviousStep(VerificationRequestedByConstructor);

            VerificationRejectedByHead
                .AddPossiblePreviousStep(VerificationRequestedByConstructor);
        }

        #endregion

        /// <summary>
        /// Есть возможность перейти на данный этап с обозначенного (в текущей роли).
        /// </summary>
        /// <param name="currentStep">Этап, с которого предполагаемо возможен переход</param>
        /// <returns></returns>
        public virtual bool AllowDoStep(ScriptStep2 currentStep)
            {
                return this.Role == GlobalAppProperties.User.RoleCurrent && 
                       PossiblePreviousSteps.Contains(currentStep);
            }

        private ScriptStep2 AddPossiblePreviousStep(ScriptStep2 step)
        {
            ((List<ScriptStep2>)this.PossiblePreviousSteps).Add(step);
            return this;
        }

        public abstract void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask);

        #region Classes

        private sealed class CreatedStep : ScriptStep2
        {
            public override string FullName => "Задача создана";

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
            }

            public CreatedStep() : base(0, Role.SalesManager)
            {
            }
        }

        private sealed class StartedStep : ScriptStep2
        {
            public override string FullName => "Задача запущена на проработку";

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStartedEvent>().Publish(priceEngineeringTask);
            }

            public StartedStep() : base(1, Role.SalesManager)
            {
            }
        }

        private sealed class StoppedStep : ScriptStep2
        {
            public override string FullName => "Задача остановлена менеджером";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(priceEngineeringTask);
            }

            public StoppedStep() : base(2, Role.SalesManager)
            {
            }
        }

        private sealed class RejectedByManagerStep : ScriptStep2
        {
            public override string FullName => "Проработка отклонена менеджером исполнителю";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(priceEngineeringTask);
            }

            public RejectedByManagerStep() : base(3, Role.SalesManager)
            {
            }
        }

        private sealed class RejectedByConstructorStep : ScriptStep2
        {
            public override string FullName => "Проработка отклонена исполнителем менеджеру";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(priceEngineeringTask);
            }

            public RejectedByConstructorStep() : base(4, Role.Constructor)
            {
            }
        }

        private sealed class FinishedByConstructorStep : ScriptStep2
        {
            public override string FullName => "Проработка завершена исполнителем";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(priceEngineeringTask);
            }

            public FinishedByConstructorStep() : base(5, Role.Constructor)
            {
            }
        }

        private sealed class AcceptedStep : ScriptStep2
        {
            public override string FullName => "Проработка задачи принята менеджером";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(priceEngineeringTask);
            }

            public AcceptedStep() : base(6, Role.SalesManager)
            {
            }
        }

        private sealed class VerificationRequestedByConstructorStep : ScriptStep2
        {
            public override string FullName => "Исполнитель направил проработку руководителю на проверку";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(priceEngineeringTask);
            }

            public VerificationRequestedByConstructorStep() : base(7, Role.Constructor)
            {
            }
        }

        private sealed class VerificationAcceptedByHeadStep : ScriptStep2
        {
            public override string FullName => "Руководитель согласовал проработку исполнителю";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(priceEngineeringTask);
            }

            public VerificationAcceptedByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }
        }

        private sealed class VerificationRejectedByHeadStep : ScriptStep2
        {
            public override string FullName => "Руководитель отклонил проработку исполнителю";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }

            public VerificationRejectedByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
            }
        }

        #endregion

        public override string ToString()
        {
            return this.FullName;
        }
    }
}