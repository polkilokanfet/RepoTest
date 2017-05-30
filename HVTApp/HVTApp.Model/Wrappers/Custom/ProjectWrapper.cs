using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        public List<FacilityWrapper> Facilities => SalesUnits.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);

        public ObservableCollection<SalesUnitsGroup> SalesUnitsGroups
        {
            get
            {
                var result = new ObservableCollection<SalesUnitsGroup>();

                foreach (var salesUnit in SalesUnits)
                {
                    bool addFlag = true;
                    foreach (var salesUnitsGroup in result)
                    {
                        if (salesUnitsGroup.SalesUnits.First().ProductionUnit.Product.HasSameParameters(salesUnit.ProductionUnit.Product))
                        {
                            salesUnitsGroup.SalesUnits.Add(salesUnit);
                            addFlag = false;
                            break;
                        }
                    }

                    if (addFlag) result.Add(new SalesUnitsGroup(new[] {salesUnit}));
                }
                return result;
            }
        }
    }
}
