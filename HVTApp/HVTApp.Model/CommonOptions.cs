using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Model
{
    public static class CommonOptions
    {
        public static Guid OurCompanyId { get; set; }
        public static int CalculationPriceTerm { get; set; } = 90;
        public static int ProductionTerm { get; set; } = 120;
        public static int AssembleTerm { get; set; } = 7;
        public static Guid StandartPaymentsConditionSetId { get; set; }

        public static User User { get; set; } = new User {RoleCurrent = Role.SalesManager};
    }
}
