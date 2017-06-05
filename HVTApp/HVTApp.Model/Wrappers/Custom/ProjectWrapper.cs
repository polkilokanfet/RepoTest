using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        public List<FacilityWrapper> Facilities => Units.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => Units.Sum(x => x.SalesUnit.Cost.Sum);

        public ObservableCollection<UnitsGroup> UnitsGroups
        {
            get
            {
                var result = new ObservableCollection<UnitsGroup>();

                foreach (var unit in Units)
                {
                    bool addFlag = true;
                    foreach (var unitsGroup in result)
                    {
                        if (unitsGroup.Units.First().ProductionsUnit.Product.HasSameParameters(unit.ProductionsUnit.Product))
                        {
                            unitsGroup.Units.Add(unit);
                            addFlag = false;
                            break;
                        }
                    }

                    if (addFlag) result.Add(new UnitsGroup(new[] {unit}));
                }
                return result;
            }
        }
    }
}
