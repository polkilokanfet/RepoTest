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

        #region Steps

        /// <summary>
        /// ������ �������
        /// </summary>
        public static readonly ScriptStep2 Create = new CreatStep();

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
        /// ���������� ��������� ������������� �� ���������
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

        private sealed class CreatStep : ScriptStep2
        {
            public override string Description => "������ �������";

            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
            }

            public CreatStep() : base(0, Role.SalesManager)
            {
            }
        }

        private sealed class StartStep : ScriptStep2
        {
            public override string Description => "������ �������� �� ����������";

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
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskStoppedEvent>().Publish(priceEngineeringTask);
            }

            public StopStep() : base(2, Role.SalesManager)
            {
            }
        }

        private sealed class RejectByManagerStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ���������� �����������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByManagerEvent>().Publish(priceEngineeringTask);
            }

            public RejectByManagerStep() : base(3, Role.SalesManager)
            {
            }
        }

        private sealed class RejectByConstructorStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ������������ ���������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskRejectedByConstructorEvent>().Publish(priceEngineeringTask);
            }

            public RejectByConstructorStep() : base(4, Role.Constructor)
            {
            }
        }

        private sealed class FinishByConstructorStep : ScriptStep2
        {
            public override string Description => "���������� ��������� ������������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedEvent>().Publish(priceEngineeringTask);
            }

            public FinishByConstructorStep() : base(5, Role.Constructor)
            {
            }
        }

        private sealed class AcceptStep : ScriptStep2
        {
            public override string Description => "���������� ������ ������� ����������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskAcceptedEvent>().Publish(priceEngineeringTask);
            }

            public AcceptStep() : base(6, Role.SalesManager)
            {
            }
        }

        private sealed class VerificationRequestByConstructorStep : ScriptStep2
        {
            public override string Description => "����������� �������� ���������� ������������ �� ��������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskFinishedGoToVerificationEvent>().Publish(priceEngineeringTask);
            }

            public VerificationRequestByConstructorStep() : base(7, Role.Constructor)
            {
            }
        }

        private sealed class VerificationAcceptByHeadStep : ScriptStep2
        {
            public override string Description => "������������ ���������� ���������� �����������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationAcceptedByHeadEvent>().Publish(priceEngineeringTask);
            }

            public VerificationAcceptByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }
        }

        private sealed class VerificationRejectByHeadStep : ScriptStep2
        {
            public override string Description => "������������ �������� ���������� �����������";
            public override void PublishEvent(IEventAggregator eventAggregator, PriceEngineeringTask priceEngineeringTask)
            {
                eventAggregator.GetEvent<PriceEngineeringTaskVerificationRejectedByHeadEvent>().Publish(priceEngineeringTask);
            }

            public VerificationRejectByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
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