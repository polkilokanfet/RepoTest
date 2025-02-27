using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitGroup : BindableBase, IProjectUnit
    {
        public IValidatableChangeTrackingCollection<ProjectUnit> Units { get; }

        #region SimpleProperties

        public double Cost
        {
            get => this.Units.First().Cost;
            set => this.Units.ForEach(projectUnit => projectUnit.Cost = value);
        }

        public double? CostDelivery
        {
            get => this.Units.First().CostDelivery;
            set => this.Units.ForEach(projectUnit => projectUnit.CostDelivery = value);
        }

        public int ProductionTerm
        {
            get => this.Units.First().ProductionTerm;
            set => this.Units.ForEach(projectUnit => projectUnit.ProductionTerm = value);
        }

        public DateTime DeliveryDateExpected
        {
            get => this.Units.First().DeliveryDateExpected;
            set => this.Units.ForEach(projectUnit => projectUnit.DeliveryDateExpected = value);
        }

        public string Comment
        {
            get => this.Units.First().Comment;
            set => this.Units.ForEach(projectUnit => projectUnit.Comment = value);
        }

        #endregion

        #region ComplexProperties

        public FacilityEmptyWrapper Facility
        {
            get => Units.First().Facility;
            set
            {
                Units.ForEach(projectUnit => projectUnit.Facility = value);
                RaisePropertyChanged();
            }
        }

        public ProductEmptyWrapper Product
        {
            get => Units.First().Product;
            set
            {
                Units.ForEach(projectUnit => projectUnit.Product = value);
                RaisePropertyChanged();
            }
        }

        public PaymentConditionSetEmptyWrapper PaymentConditionSet
        {
            get => Units.First().PaymentConditionSet;
            set
            {
                Units.ForEach(projectUnit => projectUnit.PaymentConditionSet = value);
                RaisePropertyChanged();
            }
        }

        public CompanyEmptyWrapper Producer
        {
            get => Units.First().Producer;
            set
            {
                Units.ForEach(projectUnit => projectUnit.Producer = value);
                RaisePropertyChanged();
            }
        }

        #endregion


        public Specification Specification => Units.First().Specification;

        public IEnumerable<ProjectUnitProductIncludedGroup> ProductsIncludedGroups =>
            this.Units
                .SelectMany(x => x.ProductsIncluded)
                .GroupBy(x => x, new ProjectUnitProductIncluded.ProjectUnitProductIncludedComparer())
                .Select(x => new ProjectUnitProductIncludedGroup(x))
                .OrderBy(x => x.Name);

        public void CopyProps(IProjectUnit projectUnit)
        {
            throw new NotImplementedException();
        }

        public ProjectUnitGroup(IEnumerable<ProjectUnit> projectUnits)
        {
            Units = new ValidatableChangeTrackingCollection<ProjectUnit>(projectUnits);
        }

        public bool Add(ProjectUnit projectUnit)
        {
            if (this.Units.First().HasSameGroup(projectUnit) == false) 
                return false;

            this.Units.Add(projectUnit);
            return true;
        }
    }
}