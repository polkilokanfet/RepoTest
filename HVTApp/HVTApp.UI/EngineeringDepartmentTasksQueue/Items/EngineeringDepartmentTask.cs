using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.Items
{
    public abstract class EngineeringDepartmentTask : IComparable<EngineeringDepartmentTask>
    {
        public DateTime TermOriginal => this.GetTermOriginal();
        public DateTime Term => BaseTask.TermPriority ?? TermOriginal;
        public string Facility => GetFacility();
        public string Product => GetProduct();
        public abstract string TaskType { get; }

        public IBasePriorityTask BaseTask { get; }

        protected EngineeringDepartmentTask(IBasePriorityTask baseTask)
        {
            BaseTask = baseTask;
        }

        protected abstract DateTime GetTermOriginal();
        protected abstract string GetFacility();
        protected abstract string GetProduct();

        public int CompareTo(EngineeringDepartmentTask other)
        {
            return this.Term.CompareTo(other.Term);
        }
    }
}
