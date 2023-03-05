using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using Prism.Events;

namespace HVTApp.Model.POCOs
{
    public abstract class ScriptStep : SmartEnumeration<ScriptStep>
    {
        protected readonly Role Role;

        public abstract string Description { get; }

        /// <summary>
        /// С каких этапов можно перейти на данный (в текущей роли).
        /// </summary>
        public abstract IEnumerable<ScriptStep> PossiblePreviousSteps { get; } 

        /// <summary>
        /// На какие этапы можно перейти с данного (в текущей роли).
        /// </summary>
        public IEnumerable<ScriptStep> PossibleNextSteps
        {
            get
            {
                return GetMembers()
                    .Where(step => step.Equals(this) == false)
                    .Where(step => step.AllowDoStep(this));
            }
        }

        #region ShowToRole

        /// <summary>
        /// Показывать в списке задач
        /// </summary>
        public bool Show
        {
            get
            {
                switch (GlobalAppProperties.User.RoleCurrent)
                {
                    case Role.SalesManager:
                        return this.ShowToSalesManager;

                    case Role.Constructor:
                        return this.ShowToConstructor;

                    case Role.DesignDepartmentHead:
                        return this.ShowToHead;

                    case Role.Admin:
                    case Role.Director:
                    case Role.Economist:
                    case Role.Pricer:
                    case Role.DataBaseFiller:
                    case Role.PlanMaker:
                    case Role.ReportMaker:
                    case Role.Supplier:
                    case Role.BackManager:
                    case Role.BackManagerBoss:
                        return false;
                }

                return false;
            }
        }

        /// <summary>
        /// Показывать менеджеру (значение по умолчанию - true)
        /// </summary>
        protected virtual bool ShowToSalesManager => true;
        /// <summary>
        /// Показывать конструктору (значение по умолчанию - false)
        /// </summary>
        protected virtual bool ShowToConstructor => false;
        /// <summary>
        /// Показывать руководителю КБ (значение по умолчанию - false)
        /// </summary>
        protected virtual bool ShowToHead => false;

        #endregion

        #region Steps

        /// <summary>
        /// Задача создана
        /// </summary>
        public static readonly ScriptStep Create = new CreateStep();

        /// <summary>
        /// Задача запущена на проработку
        /// </summary>
        public static readonly ScriptStep Start = new StartStep();

        /// <summary>
        /// Задача остановлена менеджером
        /// </summary>
        public static readonly ScriptStep Stop = new StopStep();

        /// <summary>
        /// Проработка отклонена менеджером конструктору
        /// </summary>
        public static readonly ScriptStep RejectByManager = new RejectByManagerStep();

        /// <summary>
        /// Проработка отклонена конструктором менеджеру
        /// </summary>
        public static readonly ScriptStep RejectByConstructor = new RejectByConstructorStep();

        /// <summary>
        /// Проработка отклонена менеджеру руководителем КБ
        /// </summary>
        public static readonly ScriptStep RejectByHead = new RejectByHeadStep();

        /// <summary>
        /// Проработка завершена конструктором
        /// </summary>
        public static readonly ScriptStep FinishByConstructor = new FinishByConstructorStep();

        /// <summary>
        /// Проработка задачи принята менеджером
        /// </summary>
        public static readonly ScriptStep Accept = new AcceptStep();

        /// <summary>
        /// Конструктор направил проработку руководителю на проверку
        /// </summary>
        public static readonly ScriptStep VerificationRequestByConstructor = new VerificationRequestByConstructorStep();

        /// <summary>
        /// Руководитель согласовал проработку конструктору
        /// </summary>
        public static readonly ScriptStep VerificationAcceptByHead = new VerificationAcceptByHeadStep();

        /// <summary>
        /// Руководитель отклонил проработку конструктору
        /// </summary>
        public static readonly ScriptStep VerificationRejectByHead = new VerificationRejectByHeadStep();

        /// <summary>
        /// Загрузить проработку в ТСЕ (старт от менеджера)
        /// </summary>
        public static readonly ScriptStep LoadToTceStart = new LoadToTceStartStep();

        /// <summary>
        /// Загрузка проработки в ТСЕ завершена
        /// </summary>
        public static readonly ScriptStep LoadToTceFinish = new LoadToTceFinishStep();

        /// <summary>
        /// Запрос на открытие производства
        /// </summary>
        public static readonly ScriptStep ProductionRequestStart = new ProductionRequestStartStep();

        /// <summary>
        /// Запрос на открытие производства обработан
        /// </summary>
        public static readonly ScriptStep ProductionRequestFinish = new ProductionRequestFinishStep();


        #endregion

        #region ctors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="role">Роль, в которой пожно перейти на данный этап</param>
        private ScriptStep(int value, Role role) : base(value)
        {
            Role = role;
        }

        #endregion

        /// <summary>
        /// Есть возможность перейти на данный этап с обозначенного (в текущей роли).
        /// </summary>
        /// <param name="currentStep">Этап, с которого предполагаемо возможен переход</param>
        /// <returns></returns>
        public virtual bool AllowDoStep(ScriptStep currentStep)
        {
            return this.Role == GlobalAppProperties.User.RoleCurrent &&
                   PossiblePreviousSteps.Contains(currentStep);
        }

        public abstract void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask);

        #region Classes

        private sealed class CreateStep : ScriptStep
        {
            public override string Description => "Задача создана";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {

            };

            public CreateStep() : base(0, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
            }
        }

        private sealed class StartStep : ScriptStep
        {
            public override string Description => "Задача запущена на проработку";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Create,
                RejectByHead,
                RejectByConstructor
            };

            protected override bool ShowToHead => true;
            protected override bool ShowToConstructor => true;

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStartedEvent>().Publish(priceEngineeringTask);
            }

            public StartStep() : base(1, Role.SalesManager)
            {
            }
        }

        private sealed class StopStep : ScriptStep
        {
            public override string Description => "Задача остановлена менеджером";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start,
                RejectByManager,
                RejectByConstructor,
                FinishByConstructor,
                VerificationRequestByConstructor,
                VerificationAcceptByHead,
                VerificationRejectByHead,
                Accept
            };

            protected override bool ShowToSalesManager => false;

            public StopStep() : base(2, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByManagerStep : ScriptStep
        {
            public override string Description => "Проработка отклонена менеджером исполнителю";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                FinishByConstructor,
                VerificationAcceptByHead
            };

            protected override bool ShowToConstructor => true;

            public RejectByManagerStep() : base(3, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByConstructorStep : ScriptStep
        {
            public override string Description => "Проработка отклонена исполнителем менеджеру";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start
            };

            public RejectByConstructorStep() : base(4, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class FinishByConstructorStep : ScriptStep
        {
            public override string Description => "Проработка завершена исполнителем";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start,
                VerificationAcceptByHead
            };

            public FinishByConstructorStep() : base(5, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class AcceptStep : ScriptStep
        {
            public override string Description => "Проработка задачи принята менеджером";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                FinishByConstructor,
                VerificationAcceptByHead
            };

            protected override bool ShowToSalesManager => false;

            public AcceptStep() : base(6, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationRequestByConstructorStep : ScriptStep
        {
            public override string Description => "Исполнитель направил проработку руководителю на проверку";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start,
                VerificationRejectByHead
            };

            protected override bool ShowToHead => true;

            public VerificationRequestByConstructorStep() : base(7, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationAcceptByHeadStep : ScriptStep
        {
            public override string Description => "Руководитель согласовал проработку исполнителю";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                VerificationRequestByConstructor
            };

            public VerificationAcceptByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationRejectByHeadStep : ScriptStep
        {
            public override string Description => "Руководитель отклонил проработку исполнителю";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                VerificationRequestByConstructor
            };

            protected override bool ShowToConstructor => true;

            public VerificationRejectByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByHeadStep : ScriptStep
        {
            public override string Description => "Руководитель КБ отклонил задачу менеджеру";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start
            };

            public RejectByHeadStep() : base(10, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class LoadToTceStartStep : ScriptStep
        {
            public override string Description => "Менеджер поставил задачу загрузить проработку в ТС";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Accept
            };

            public LoadToTceStartStep() : base(11, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskLoadToTceStartEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class LoadToTceFinishStep : ScriptStep
        {
            public override string Description => "Загрузка проработки в ТС завершена";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                LoadToTceStart
            };

            public LoadToTceFinishStep() : base(12, Role.BackManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskLoadToTceFinishEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class ProductionRequestStartStep : ScriptStep
        {
            public override string Description => "Запрос на открытие производства";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Accept,
                LoadToTceFinish
            };

            public ProductionRequestStartStep() : base(13, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskProductionRequestStartEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class ProductionRequestFinishStep : ScriptStep
        {
            public override string Description => "Производство открыто";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new []
            {
                ProductionRequestStart
            };

            public ProductionRequestFinishStep() : base(14, Role.BackManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskProductionRequestFinishEvent>().Publish(priceEngineeringTask);
            }
        }

        #endregion

        public override string ToString()
        {
            return this.Description;
        }
    }
}