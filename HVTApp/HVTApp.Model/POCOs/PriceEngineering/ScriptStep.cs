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
        /// � ����� ������ ����� ������� �� ������ (� ������� ����).
        /// </summary>
        public abstract IEnumerable<ScriptStep> PossiblePreviousSteps { get; } 

        /// <summary>
        /// �� ����� ����� ����� ������� � ������� (� ������� ����).
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
        /// ���������� � ������ �����
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
        /// ���������� ��������� (�������� �� ��������� - true)
        /// </summary>
        protected virtual bool ShowToSalesManager => true;
        /// <summary>
        /// ���������� ������������ (�������� �� ��������� - false)
        /// </summary>
        protected virtual bool ShowToConstructor => false;
        /// <summary>
        /// ���������� ������������ �� (�������� �� ��������� - false)
        /// </summary>
        protected virtual bool ShowToHead => false;

        #endregion

        #region Steps

        /// <summary>
        /// ������ �������
        /// </summary>
        public static readonly ScriptStep Create = new CreateStep();

        /// <summary>
        /// ������ �������� �� ����������
        /// </summary>
        public static readonly ScriptStep Start = new StartStep();

        /// <summary>
        /// ������ ����������� ����������
        /// </summary>
        public static readonly ScriptStep Stop = new StopStep();

        /// <summary>
        /// ���������� ��������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep RejectByManager = new RejectByManagerStep();

        /// <summary>
        /// ���������� ��������� ������������� ���������
        /// </summary>
        public static readonly ScriptStep RejectByConstructor = new RejectByConstructorStep();

        /// <summary>
        /// ���������� ��������� ��������� ������������� ��
        /// </summary>
        public static readonly ScriptStep RejectByHead = new RejectByHeadStep();

        /// <summary>
        /// ���������� ��������� �������������
        /// </summary>
        public static readonly ScriptStep FinishByConstructor = new FinishByConstructorStep();

        /// <summary>
        /// ���������� ������ ������� ����������
        /// </summary>
        public static readonly ScriptStep Accept = new AcceptStep();

        /// <summary>
        /// ����������� �������� ���������� ������������ �� ��������
        /// </summary>
        public static readonly ScriptStep VerificationRequestByConstructor = new VerificationRequestByConstructorStep();

        /// <summary>
        /// ������������ ���������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep VerificationAcceptByHead = new VerificationAcceptByHeadStep();

        /// <summary>
        /// ������������ �������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep VerificationRejectByHead = new VerificationRejectByHeadStep();

        /// <summary>
        /// ��������� ���������� � ��� (����� �� ���������)
        /// </summary>
        public static readonly ScriptStep LoadToTceStart = new LoadToTceStartStep();

        /// <summary>
        /// �������� ���������� � ��� ���������
        /// </summary>
        public static readonly ScriptStep LoadToTceFinish = new LoadToTceFinishStep();

        /// <summary>
        /// ������ �� �������� ������������
        /// </summary>
        public static readonly ScriptStep ProductionRequestStart = new ProductionRequestStartStep();

        /// <summary>
        /// ������ �� �������� ������������ ���������
        /// </summary>
        public static readonly ScriptStep ProductionRequestFinish = new ProductionRequestFinishStep();


        #endregion

        #region ctors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="role">����, � ������� ����� ������� �� ������ ����</param>
        private ScriptStep(int value, Role role) : base(value)
        {
            Role = role;
        }

        #endregion

        /// <summary>
        /// ���� ����������� ������� �� ������ ���� � ������������� (� ������� ����).
        /// </summary>
        /// <param name="currentStep">����, � �������� ������������� �������� �������</param>
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
            public override string Description => "������ �������";

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
            public override string Description => "������ �������� �� ����������";

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
            public override string Description => "������ ����������� ����������";

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
            public override string Description => "���������� ��������� ���������� �����������";

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
            public override string Description => "���������� ��������� ������������ ���������";

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
            public override string Description => "���������� ��������� ������������";

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
            public override string Description => "���������� ������ ������� ����������";

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
            public override string Description => "����������� �������� ���������� ������������ �� ��������";

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
            public override string Description => "������������ ���������� ���������� �����������";

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
            public override string Description => "������������ �������� ���������� �����������";

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
            public override string Description => "������������ �� �������� ������ ���������";

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
            public override string Description => "�������� �������� ������ ��������� ���������� � ��";

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
            public override string Description => "�������� ���������� � �� ���������";

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
            public override string Description => "������ �� �������� ������������";

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
            public override string Description => "������������ �������";

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