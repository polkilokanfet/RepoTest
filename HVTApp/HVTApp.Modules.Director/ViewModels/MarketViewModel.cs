using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Director.ViewModels
{
    public class MarketViewModel : ViewModelBase
    {
        public List<MarketUnit> MarketUnits { get; private set; }

        public MarketViewModel(IUnityContainer container) : base(container)
        {
            Load();
        }

        private void Load()
        {
            var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Project.ForReport);
            MarketUnits = salesUnits
                .GroupBy(x => new {x.Project.Id, x.OrderInTakeDate})
                .OrderBy(x => x.Key.OrderInTakeDate)
                .Select(x => new MarketUnit(x))
                .ToList();
        }
    }

    public class MarketUnit
    {
        public string Project { get; }
        public string Facilities { get; }
        public double Sum { get; }
        public DateTime OrderInTakeDate { get; }
        public string Manager { get; }

        public List<SalesGroup> SalesGroups { get; }

        public MarketUnit(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsList = salesUnits.ToList();

            Project = salesUnitsList.First().Project.ToString();

            var builder = new StringBuilder();
            salesUnitsList.Select(x => x.Facility).Distinct().ForEach(x => builder.Append("; ").Append($"{x}"));
            Facilities = builder.Remove(0, 2).ToString();

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

    public class SalesGroup
    {
        public string Facility { get; }
        public string ProductType { get; }
        public string ProductDesignation { get; }
        public double Cost { get; }
        public int Amount { get; }
        public double Sum { get; set; }

        public SalesGroup(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnitsList = salesUnits.ToList();

            Facility = salesUnitsList.First().Facility.ToString();
            ProductType = salesUnitsList.First().Product.ProductType.ToString();
            ProductDesignation = salesUnitsList.First().Product.Designation;
            Cost = salesUnitsList.First().Cost;
            Amount = salesUnitsList.Count;
            Sum = Cost * Amount;
        }
    }

}
