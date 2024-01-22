using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Director.ViewModels
{
    public class MarketUnit
    {
        public string Project { get; }
        public string Facilities { get; }
        public double Sum { get; }
        public DateTime OrderInTakeDate { get; }
        public string Manager { get; }
        public int OrderInTakeYear => OrderInTakeDate.Year;
        public string OrderInTakeMonth => OrderInTakeDate.MonthName();


        public List<SalesGroup> SalesGroups { get; }

        public MarketUnit(IEnumerable<SalesUnit> salesUnits)
        {
            if (salesUnits == null) throw new NullReferenceException($"{nameof(salesUnits)} не должен быть null");
            var salesUnitsList = salesUnits.ToList();
            if (!salesUnitsList.Any()) throw new ArgumentException($@"{nameof(salesUnits)} не имеет членов", nameof(salesUnits));

            Project = salesUnitsList.First().Project.ToString();
            Facilities = salesUnitsList.Select(x => x.Facility).ToStringEnum(); 
            Sum = salesUnitsList.Sum(x => x.Cost);
            OrderInTakeDate = salesUnitsList.First().OrderInTakeDate;
            Manager = salesUnitsList.First().Project.Manager.Employee.Person.ToString();

            SalesGroups = salesUnitsList.GroupBy(x => new
                {
                    x.Cost,
                    FacilityId = x.Facility.Id,
                    ProductId = x.Product.Id
                })
                .Select(x => new SalesGroup(x))
                .OrderByDescending(x => x.Sum)
                .ToList();
        }
    }
}