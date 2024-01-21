using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

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

        /// <summary>
        /// ���������� � ������ �����
        /// </summary>
        public bool Show => RolesForShow.Contains(GlobalAppProperties.User.RoleCurrent);

        protected abstract IEnumerable<Role> RolesForShow { get; }

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

        /// <summary>
        /// ������ �� ��������� ������������
        /// </summary>
        public static readonly ScriptStep ProductionRequestStop = new ProductionRequestStopStep();

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

        #region Classes

        private sealed class CreateStep : ScriptStep
        {
            public override string Description => "������ �������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>();

            protected override IEnumerable<Role> RolesForShow => new List<Role>();

            public CreateStep() : base(0, Role.SalesManager)
            {
            }
        }

        private sealed class StartStep : ScriptStep
        {
            public override string Description => "������ �������� �� ����������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new []
            {
                Create, 
                Stop,
                RejectByHead,
                RejectByConstructor
            };

            protected override IEnumerable<Role> RolesForShow => new []
            {
                Role.SalesManager,
                Role.Constructor,
                Role.DesignDepartmentHead
            };

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
                Accept, 
                LoadToTceStart,
                LoadToTceFinish,
                ProductionRequestStart
            };

            protected override IEnumerable<Role> RolesForShow => new List<Role>();

            public StopStep() : base(2, Role.SalesManager)
            {
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

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager,
                Role.Constructor
            };

            public RejectByManagerStep() : base(3, Role.SalesManager)
            {
            }
        }

        private sealed class RejectByConstructorStep : ScriptStep
        {
            public override string Description => "���������� ��������� ������������ ���������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start
            };

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager
            };

            public RejectByConstructorStep() : base(4, Role.Constructor)
            {
            }
        }

        private sealed class FinishByConstructorStep : ScriptStep
        {
            public override string Description => "���������� ��������� ������������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start,
                RejectByManager,
                VerificationRejectByHead,
                VerificationAcceptByHead
            };

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager
            };

            public FinishByConstructorStep() : base(5, Role.Constructor)
            {
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

            protected override IEnumerable<Role> RolesForShow => new List<Role>();

            public AcceptStep() : base(6, Role.SalesManager)
            {
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

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager,
                Role.DesignDepartmentHead
            };

            public VerificationRequestByConstructorStep() : base(7, Role.Constructor)
            {
            }
        }

        private sealed class VerificationAcceptByHeadStep : ScriptStep
        {
            public override string Description => "������������ ���������� ���������� �����������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                VerificationRequestByConstructor
            };

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager
            };

            public VerificationAcceptByHeadStep() : base(8, Role.DesignDepartmentHead)
            {
            }
        }

        private sealed class VerificationRejectByHeadStep : ScriptStep
        {
            public override string Description => "������������ �������� ���������� �����������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                VerificationRequestByConstructor
            };

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.Constructor
            };

            public VerificationRejectByHeadStep() : base(9, Role.DesignDepartmentHead)
            {
            }
        }

        private sealed class RejectByHeadStep : ScriptStep
        {
            public override string Description => "������������ �� �������� ������ ���������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Start
            };

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager
            };

            public RejectByHeadStep() : base(10, Role.DesignDepartmentHead)
            {
            }
        }

        private sealed class LoadToTceStartStep : ScriptStep
        {
            public override string Description => "�������� �������� ������ ��������� ���������� � ��";

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.SalesManager,
                Role.BackManager,
                Role.BackManagerBoss
            };

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                Accept
            };

            public LoadToTceStartStep() : base(11, Role.SalesManager)
            {
            }
        }

        private sealed class LoadToTceFinishStep : ScriptStep
        {
            public override string Description => "�������� ���������� � �� ���������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new List<ScriptStep>
            {
                LoadToTceStart
            };

            protected override IEnumerable<Role> RolesForShow => new List<Role>();

            public LoadToTceFinishStep() : base(12, Role.BackManager)
            {
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

            protected override IEnumerable<Role> RolesForShow => new[]
            {
                Role.BackManagerBoss,
                Role.PlanMaker
            };

            public ProductionRequestStartStep() : base(13, Role.SalesManager)
            {
            }
        }

        private sealed class ProductionRequestFinishStep : ScriptStep
        {
            public override string Description => "������������ �������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new[]
            {
                ProductionRequestStart
            };

            protected override IEnumerable<Role> RolesForShow => new List<Role>();

            public ProductionRequestFinishStep() : base(14, Role.PlanMaker)
            {
            }
        }

        private sealed class ProductionRequestStopStep : ScriptStep
        {
            public override string Description => "������ �� ��������� ������������";

            public override IEnumerable<ScriptStep> PossiblePreviousSteps => new[]
            {
                ProductionRequestFinish
            };

            protected override IEnumerable<Role> RolesForShow => new List<Role>()
            {
                Role.SalesManager,
                Role.BackManagerBoss
            };

            public ProductionRequestStopStep() : base(15, Role.SalesManager)
            {
            }
        }

        #endregion

        public override string ToString()
        {
            return this.Description;
        }
    }
}