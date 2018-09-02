using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class CommonOption : BaseEntity
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public Guid OurCompanyId { get; set; }
        public int ActualPriceTerm { get; set; } = 90;
        public int StandartTermFromStartToEndProduction { get; set; } = 120;
        public int StandartTermFromPickToEndProduction { get; set; } = 7;
        public Guid StandartPaymentsConditionSetId { get; set; }
    }
}