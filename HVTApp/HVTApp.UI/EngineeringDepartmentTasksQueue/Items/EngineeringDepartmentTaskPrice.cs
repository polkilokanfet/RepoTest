using System;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.Items
{
    public class EngineeringDepartmentTaskPrice : EngineeringDepartmentTask
    {
        public override string TaskType => "ТКП";

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