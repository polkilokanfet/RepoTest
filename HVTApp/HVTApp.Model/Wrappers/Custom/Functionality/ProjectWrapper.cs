using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        protected override void RunInConstructor()
        {
            ProductsUnitsGroups = new ProductsUnitsGroupsCollection<ProductComplexUnitWrapper>(ProductComplexUnits);
        }

        public List<FacilityWrapper> Facilities => ProductComplexUnits.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => ProductComplexUnits.Sum(x => x.Cost.Sum);

        public ProductsUnitsGroupsCollection<ProductComplexUnitWrapper> ProductsUnitsGroups { get; private set; }
    }
}
