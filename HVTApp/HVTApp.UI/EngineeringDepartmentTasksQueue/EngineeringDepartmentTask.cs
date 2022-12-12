using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue
{
    public abstract class EngineeringDepartmentTask : IComparable<EngineeringDepartmentTask>
    {
        public DateTime TermOriginal => this.GetTermOriginal();
        public DateTime Term => BaseTask.TermPriority ?? TermOriginal;
        public string Facility => GetFacility();
        public string Product => GetProduct();

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

    public class EngineeringDepartmentTaskPrice : EngineeringDepartmentTask
    {
        private readonly DateTime _termOriginal;
        private readonly string _facility;
        private readonly string _product;

        public EngineeringDepartmentTaskPrice(PriceEngineeringTask baseTask, DateTime termOriginal) : base(baseTask)
        {
            _termOriginal = termOriginal;
            _facility = baseTask.SalesUnits.First().Facility.ToString();
            _product = baseTask.ProductBlock.ToString();
        }

        protected override DateTime GetTermOriginal()
        {
            return _termOriginal;
        }

        protected override string GetFacility()
        {
            return _facility;
        }

        protected override string GetProduct()
        {
            return _product;
        }
    }
}
