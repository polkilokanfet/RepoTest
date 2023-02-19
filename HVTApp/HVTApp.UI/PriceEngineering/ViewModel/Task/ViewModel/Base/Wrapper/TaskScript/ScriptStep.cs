using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Wrapper.TaskScript
{
    /// <summary>
    /// ���� ���������� ������
    /// </summary>
    public abstract class ScriptStep<TTaskViewModel> : IScriptStep
        where TTaskViewModel : TaskViewModel
    {
        protected readonly TTaskViewModel ViewModel;

        /// <summary>
        /// �����, ������� ����� ���� �� ����
        /// </summary>
        private readonly IList<IScriptStep> _nextSteps = new List<IScriptStep>();

        /// <summary>
        /// ������� ������ ������
        /// </summary>
        public PriceEngineeringTaskStatusEnum Status { get; }

        /// <summary>
        /// ���� ������������, ������� ����� ������� �� ���� ����
        /// </summary>
        public Role Role { get; }

        protected ScriptStep(PriceEngineeringTaskStatusEnum status, Role role, TTaskViewModel viewModel)
        {
            ViewModel = viewModel;
            Status = status;
            Role = role;
        }

        /// <summary>
        /// ���������� ���������� ���������� �����
        /// </summary>
        /// <param name="step"></param>
        protected void AddNextStep(IScriptStep step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            if (step.Status == this.Status) throw new ArgumentException(nameof(step));
            if (_nextSteps.Contains(step)) throw new ArgumentException(nameof(step));

            _nextSteps.Add(step);
        }

        public virtual bool AllowNextStep(PriceEngineeringTaskStatusEnum status)
        {
            if (ViewModel.IsTarget == false) return false;
            if (ViewModel.IsEditMode == false) return false;
            if (_nextSteps.Select(x => x.Status).Contains(status) == false) return false;
            
            return true;
        }

        public bool DoNextStep(PriceEngineeringTaskStatusEnum status)
        {
            if (AllowNextStep(status))
            {
                ViewModel.Statuses.Add(status);
                return true;
            }

            return false;
        }
    }
}