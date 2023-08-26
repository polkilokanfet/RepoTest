using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.FairnessCheck
{
    public class FairnessCheckItem
    {
        private readonly SalesUnit[] _salesUnits;

        public SalesUnit SalesUnit => _salesUnits.First();
        public int Amount => _salesUnits.Length;

        public FairnessCheckItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits as SalesUnit[] ?? salesUnits.ToArray();
        }
    }
}