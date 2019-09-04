using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Wrapper;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels.Groups
{
    public static class ConverterExt
    {
        public static IEnumerable<DatesGroup> ConvertToGroups(this IEnumerable<SalesUnitWrapper> units)
        {
            var groups = units.GroupBy(x => new
            {
                Facility = x.Facility.Id,
                Product = x.Product.Id,
                Order = x.Order?.Id,
                Project = x.Project.Id,
                Specification = x.Specification?.Id,
                x.DeliveryDate,
                x.EndProductionDate,
                x.PickingDate,
                x.RealizationDate,
                x.ShipmentDate
            });

            return groups.Select(x => new DatesGroup(x));
        }
    }
}