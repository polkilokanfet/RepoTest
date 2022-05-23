using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.PriceEngineering.Tce.Unit
{
    public class PriceEngineeringTaskTceItem
    {
        public List<TceStructureCostVersion> TceStructureCostVersions { get; }

        public PriceEngineeringTaskTceItem(IEnumerable<TceStructureCostVersion> tceStructureCostVersions)
        {
            TceStructureCostVersions = tceStructureCostVersions.ToList();
        }
    }
}