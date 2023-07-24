using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public class ProjectUnitsGroup : 
        BaseWrappersGroup<ProjectUnitsGroup, SalesUnit, ProjectUnit>, 
        IGroupValidatableChangeTrackingWithCollection<ProjectUnitsGroup, SalesUnit>
    {
        public List<SalesUnit> SalesUnits { get; }

        public bool CanRemove => SalesUnits.All(salesUnit => salesUnit.Order == null);
        public bool CanTotalRemove => SalesUnits.All(salesUnit => salesUnit.AllowTotalRemove);

        public ProjectSimpleWrapper Project
        {
            get => GetValue<ProjectSimpleWrapper>();
            set => SetValue(value);
        }

        public CompanySimpleWrapper Producer
        {
            get => GetValue<CompanySimpleWrapper>();
            set => SetValue(value);
        }

        public SpecificationSimpleWrapper Specification
        {
            get => GetValue<SpecificationSimpleWrapper>();
            set => SetValue(value);
        }

        public DateTime OrderInTakeDate => GetValue<DateTime>();

        public DateTime RealizationDateCalculated => Model.RealizationDateCalculated;

        public string TceRequest
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool IsRemoved
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public DateTime SignalToStartProduction
        {
            set => SetValue(value);
        }

        public DateTime EndProductionDateCalculated => GetValue<DateTime>();

        public DateTime DeliveryDateExpected
        {
            get => GetValue<DateTime>();
            set
            {
                //if(value < DateTime.Today) return;
                if (value > DateTime.Today.AddYears(50)) return;
                SetValue(value);
            }
        }

        public ProductType ProductType => Product.Model.ProductType;

        public ProjectUnitsGroup(List<SalesUnit> units) : base(units)
        {
            SalesUnits = units;
        }

        protected override SalesUnit GetSalesUnit()
        {
            return Model;
        }

        public override string ToString()
        {
            return $"ProjectUnitsGroup: {Facility} = {Product} = {Amount} רע.";
        }
    }
}