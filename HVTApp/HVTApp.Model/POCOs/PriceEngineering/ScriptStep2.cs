using System;
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

        public abstract string Description { get; }

        /// <summary>
        /// � ����� ������ ����� ������� �� ������ (� ������� ����).
        /// </summary>
        public IEnumerable<ScriptStep2> PossiblePreviousSteps { get; } = new List<ScriptStep2>();

        /// <summary>
        /// �� ����� ����� ����� ������� � ������� (� ������� ����).
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
        public static readonly ScriptStep2 Create = new CreateStep();

        /// <summary>
        /// ������ �������� �� ����������
        /// </summary>
        public static readonly ScriptStep2 Start = new StartStep();

        /// <summary>
        /// ������ ����������� ����������
        /// </summary>
        public static readonly ScriptStep2 Stop = new StopStep();

        /// <summary>
        /// ���������� ��������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 RejectByManager = new RejectByManagerStep();

        /// <summary>
        /// ���������� ��������� ������������� ���������
        /// </summary>
        public static readonly ScriptStep2 RejectByConstructor = new RejectByConstructorStep();

        /// <summary>
        /// ���������� ��������� ��������� ������������� ��
        /// </summary>
        public static readonly ScriptStep2 RejectByHead = new RejectByHeadStep();

        /// <summary>
        /// ���������� ��������� �������������
        /// </summary>
        public static readonly ScriptStep2 FinishByConstructor = new FinishByConstructorStep();

        /// <summary>
        /// ���������� ������ ������� ����������
        /// </summary>
        public static readonly ScriptStep2 Accept = new AcceptStep();

        /// <summary>
        /// ����������� �������� ���������� ������������ �� ��������
        /// </summary>
        public static readonly ScriptStep2 VerificationRequestByConstructor = new VerificationRequestByConstructorStep();

        /// <summary>
        /// ������������ ���������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 VerificationAcceptByHead = new VerificationAcceptByHeadStep();

        /// <summary>
        /// ������������ �������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 VerificationRejectByHead = new VerificationRejectByHeadStep();

        #endregion

        #region ctors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="role">����, � ������� ����� ������� �� ������ ����</param>
        private ScriptStep2(int value, Role role) : base(value)
        {
            Role = role;
        }

        static ScriptStep2()
        {
            Start
                .AddPossiblePreviousStep(Create)
                .AddPossiblePreviousStep(RejectByHead)
                .AddPossiblePreviousStep(RejectByConstructor);

            Stop
                .AddPossiblePreviousStep(Start)
                .AddPossiblePreviousStep(RejectByManager)
                .AddPossiblePreviousStep(RejectByConstructor)
                .AddPossiblePreviousStep(FinishByConstructor)
                .AddPossiblePreviousStep(VerificationRequestByConstructor)
                .AddPossiblePreviousStep(VerificationAcceptByHead)
                .AddPossiblePreviousStep(VerificationRejectByHead);

            RejectByManager
                .AddPossiblePreviousStep(FinishByConstructor)
                .AddPossiblePreviousStep(VerificationAcceptByHead);

            RejectByHead
                .AddPossiblePreviousStep(Start);

            RejectByConstructor
                .AddPossiblePreviousStep(Start);

            FinishByConstructor
                .AddPossiblePreviousStep(Start)
                .AddPossiblePreviousStep(VerificationAcceptByHead);

            Accept
                .AddPossiblePreviousStep(FinishByConstructor)
                .AddPossiblePreviousStep(VerificationAcceptByHead);

            VerificationRequestByConstructor
                .AddPossiblePreviousStep(Start)
                .AddPossiblePreviousStep(VerificationRejectByHead);

            VerificationAcceptByHead
                .AddPossiblePreviousStep(VerificationRequestByConstructor);

            VerificationRejectByHead
                .AddPossiblePreviousStep(VerificationRequestByConstructor);
        }

        #endregion

        /// <summary>
        /// ���� ����������� ������� �� ������ ���� � ������������� (� ������� ����).
        /// </summary>
        /// <param name="currentStep">����, � �������� ������������� �������� �������</param>
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

        private sealed class CreateStep : ScriptStep2
        {
            public override string Description => "������ �������";

            public CreateStep() : base(0, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
            }
        }

        private sealed class StartStep : ScriptStep2
        {
            public override string Description => "������ �������� �� ����������";

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

        private sealed class StopStep : ScriptStep2
        {
            public override string Description => "������ ����������� ����������";

            protected override bool ShowToSalesManager => false;

            public StopStep() : base(2, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByManagerStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ���������� �����������";

            protected override bool ShowToConstructor => true;

            public RejectByManagerStep() : base(3, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByConstructorStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ������������ ���������";

            public RejectByConstructorStep() : base(4, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class FinishByConstructorStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ������������";

            public FinishByConstructorStep() : base(5, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class AcceptStep : ScriptStep2
        {
            public override string Description => "���������� ������ ������� ����������";

            protected override bool ShowToSalesManager => false;

            public AcceptStep() : base(6, Role.SalesManager)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationRequestByConstructorStep : ScriptStep2
        {
            public override string Description => "����������� �������� ���������� ������������ �� ��������";

            protected override bool ShowToHead => true;

            public VerificationRequestByConstructorStep() : base(7, Role.Constructor)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationAcceptByHeadStep : ScriptStep2
        {
            public override string Description => "������������ ���������� ���������� �����������";

            public VerificationAcceptByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class VerificationRejectByHeadStep : ScriptStep2
        {
            public override string Description => "������������ �������� ���������� �����������";

            protected override bool ShowToConstructor => true;

            public VerificationRejectByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        private sealed class RejectByHeadStep : ScriptStep2
        {
            public override string Description => "������������ �� �������� ������ ���������";

            public RejectByHeadStep() : base(10, Role.DesignDepartmentHead)
            {
            }

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }
        }

        #endregion

        public override string ToString()
        {
            return this.Description;
        }
    }
}