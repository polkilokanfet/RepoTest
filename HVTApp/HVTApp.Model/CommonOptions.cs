using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class CommonOptions
    {
        public static Guid OurCompanyId { get; set; }
        public static int CalculationPriceTerm { get; set; } = 90;
        public static int StandartTermFromStartToEndProduction { get; set; } = 120;
        public static int StandartTermFromPickToEndProduction { get; set; } = 7;
        public static Guid StandartPaymentsConditionSetId { get; set; }
    }
}
