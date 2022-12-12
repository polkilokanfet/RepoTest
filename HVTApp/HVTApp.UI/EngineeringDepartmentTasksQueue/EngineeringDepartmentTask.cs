using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue
{
    public abstract class EngineeringDepartmentTask : IComparable<EngineeringDepartmentTask>
    {
        public DateTime Term => this.GetTerm();

        public IBaseTask BaseTask { get; }

        protected EngineeringDepartmentTask(IBaseTask baseTask)
        {
            BaseTask = baseTask;
        }

        protected abstract DateTime GetTerm();

        public int CompareTo(EngineeringDepartmentTask other)
        {
            return this.Term.CompareTo(other.Term);
        }
    }

    public class EngineeringDepartmentTaskPrice : EngineeringDepartmentTask
    {
        public EngineeringDepartmentTaskPrice(PriceEngineeringTask baseTask) : base(baseTask)
        {
        }

        protected override DateTime GetTerm()
        {
            throw new NotImplementedException();
            //return this.BaseTask.Term ?? ((PriceEngineeringTask)this.BaseTask).W
        }
    }
}
