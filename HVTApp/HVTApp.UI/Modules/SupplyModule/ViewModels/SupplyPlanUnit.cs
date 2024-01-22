using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.SupplyModule.ViewModels
{
    public class SupplyPlanUnit
    {
        private readonly List<SalesUnit> _salesUnits;

        public Product Product { get; }
        public string Voltage { get; }
        public int Amount => _salesUnits.Count;
        public DateTime SupplyDate { get; }
        public int SupplyYear => SupplyDate.Year;
        public string SupplyMonth => SupplyDate.MonthName();
        public string SupplyWeek => SupplyDate.WeekNumberString();

        public SupplyPlanUnit(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();

            var salesUnit = _salesUnits.First();
            Product = salesUnit.Product;
            Voltage = Product.GetVoltageParameter()?.Value;
            var assembleTerm = salesUnit.AssembleTerm ?? GlobalAppProperties.Actual.StandartTermFromPickToEndProduction;
            SupplyDate = salesUnit.EndProductionDateCalculated.AddDays(-1.0 * assembleTerm);
        }
    }
}