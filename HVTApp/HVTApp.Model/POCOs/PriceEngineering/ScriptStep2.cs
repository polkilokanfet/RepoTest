using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public abstract class ScriptStep2 : SmartEnumeration<ScriptStep2>
    {
        protected readonly Role Role;

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
        public static readonly ScriptStep2 Created = new CreatedStep();

        /// <summary>
        /// ������ �������� �� ����������
        /// </summary>
        public static readonly ScriptStep2 Started = new StartedStep();

        /// <summary>
        /// ������ ����������� ����������
        /// </summary>
        public static readonly ScriptStep2 Stopped = new StoppedStep();

        /// <summary>
        /// ���������� ��������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 RejectedByManager = new RejectedByManagerStep();

        /// <summary>
        /// ���������� ��������� ������������� ���������
        /// </summary>
        public static readonly ScriptStep2 RejectedByConstructor = new RejectedByConstructorStep();

        /// <summary>
        /// ���������� ��������� �������������
        /// </summary>
        public static readonly ScriptStep2 FinishedByConstructor = new FinishedByConstructorStep();

        /// <summary>
        /// ���������� ������ ������� ����������
        /// </summary>
        public static readonly ScriptStep2 Accepted = new AcceptedStep();

        /// <summary>
        /// ����������� �������� ���������� ������������ �� ��������
        /// </summary>
        public static readonly ScriptStep2 VerificationRequestedByConstructor = new VerificationRequestedByConstructorStep();

        /// <summary>
        /// ������������ ���������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 VerificationAcceptedByHead = new VerificationAcceptedByHeadStep();

        /// <summary>
        /// ������������ �������� ���������� ������������
        /// </summary>
        public static readonly ScriptStep2 VerificationRejectedByHead = new VerificationRejectedByHeadStep();

        #endregion

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
                .AddPossiblePreviousStep(Started);

            VerificationAcceptedByHead
                .AddPossiblePreviousStep(VerificationRequestedByConstructor);

            VerificationRejectedByHead
                .AddPossiblePreviousStep(VerificationRequestedByConstructor);
        }

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

        #region Classes

        /// <summary>
        /// ������ �������
        /// </summary>
        private sealed class CreatedStep : ScriptStep2
        {
            public CreatedStep() : base(0, Role.SalesManager)
            {
            }
        }
        /// <summary>
        /// ������ �������� �� ����������
        /// </summary>
        private sealed class StartedStep : ScriptStep2
        {
            public StartedStep() : base(1, Role.SalesManager)
            {
            }
        }
        /// <summary>
        /// ������ ����������� ����������
        /// </summary>
        private sealed class StoppedStep : ScriptStep2
        {
            public StoppedStep() : base(2, Role.SalesManager)
            {
            }
        }
        /// <summary>
        /// ���������� ��������� ���������� ������������
        /// </summary>
        private sealed class RejectedByManagerStep : ScriptStep2
        {
            public RejectedByManagerStep() : base(3, Role.SalesManager)
            {
            }
        }
        /// <summary>
        /// ���������� ��������� ������������� ���������
        /// </summary>
        private sealed class RejectedByConstructorStep : ScriptStep2
        {
            public RejectedByConstructorStep() : base(4, Role.Constructor)
            {
            }
        }
        /// <summary>
        /// ���������� ��������� �������������
        /// </summary>
        private sealed class FinishedByConstructorStep : ScriptStep2
        {
            public FinishedByConstructorStep() : base(5, Role.Constructor)
            {
            }
        }
        /// <summary>
        /// ���������� ������ ������� ����������
        /// </summary>
        private sealed class AcceptedStep : ScriptStep2
        {
            public AcceptedStep() : base(6, Role.SalesManager)
            {
            }
        }
        /// <summary>
        /// ����������� �������� ���������� ������������ �� ��������
        /// </summary>
        private sealed class VerificationRequestedByConstructorStep : ScriptStep2
        {
            public VerificationRequestedByConstructorStep() : base(7, Role.Constructor)
            {
            }
        }
        /// <summary>
        /// ������������ ���������� ���������� ������������
        /// </summary>
        private sealed class VerificationAcceptedByHeadStep : ScriptStep2
        {
            public VerificationAcceptedByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }
        }
        /// <summary>
        /// ������������ �������� ���������� ������������
        /// </summary>
        private sealed class VerificationRejectedByHeadStep : ScriptStep2
        {
            public VerificationRejectedByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
            }
        }

        #endregion
    }
}