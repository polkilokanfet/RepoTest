using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CommonOption : BaseEntity
    {
        public Guid OurCompanyId { get; set; }
        public int CalculationPriceTerm { get; set; } = 90;
        public int StandartTermFromStartToEndProduction { get; set; } = 120;
        public int StandartTermFromPickToEndProduction { get; set; } = 7;
        public Guid StandartPaymentsConditionSetId { get; set; }
    }
}