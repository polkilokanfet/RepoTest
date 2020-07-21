using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProjectUnitGroup : ValidatableChangeTrackingCollection<ProjectUnit>, IProjectUnit
    {
        public bool CanRemove => this.All(x => x.CanRemove);

        public double Cost { get; set; }
        public int ProductionTerm { get; set; }
        public DateTime DeliveryDateExpected { get; set; }
        public double? CostDelivery { get; set; }
        public FacilityWrapper Facility { get; set; }
        public ProductWrapper Product { get; set; }
        public PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        public CompanyWrapper Producer { get; set; }

        public IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }

        public ProjectUnitGroup(IEnumerable<SalesUnit> units) : base(units.Select(x => new ProjectUnit(x)))
        {
            ProductsIncluded = new PoductsIncludedCollection(this);
        }

        public SalesUnit Model => this.FirstOrDefault()?.Model;
        public double Price { get; set; }
        public double FixedCost { get; set; }

        public double Total => this.Sum(x => x.Cost);
    }
}