using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitGroup : BindableBase, IProjectUnit
    {
        public IValidatableChangeTrackingCollection<ProjectUnit> Units { get; }

        public double Cost
        {
            get => this.Units.First().Cost;
            set => this.Units.ForEach(x => x.Cost = value);
        }

        public double? CostDelivery
        {
            get => this.Units.First().CostDelivery;
            set => this.Units.ForEach(x => x.CostDelivery = value);
        }

        public int ProductionTerm
        {
            get => this.Units.First().ProductionTerm;
            set => this.Units.ForEach(x => x.ProductionTerm = value);
        }

        public DateTime DeliveryDateExpected
        {
            get => this.Units.First().DeliveryDateExpected;
            set => this.Units.ForEach(x => x.DeliveryDateExpected = value);
        }

        public string Comment
        {
            get => this.Units.First().Comment;
            set => this.Units.ForEach(x => x.Comment = value);
        }

        #region Facility

        public Facility Facility
        {
            get => Units.First().Facility;
            set
            {
                Units.ForEach(x => x.Facility = value);
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Product

        public Product Product
        {
            get => Units.First().Product;
            set
            {
                Units.ForEach(x => x.Product = value);
                RaisePropertyChanged();
            }
        }

        public Project Project { get; set; }

        public PaymentConditionSet PaymentConditionSet { get; set; }
        public Company Producer { get; set; }

        #endregion

        public ProjectUnitGroup(IEnumerable<ProjectUnit> salesUnits)
        {
            Units = new ValidatableChangeTrackingCollection<ProjectUnit>(salesUnits.Select(salesUnit => salesUnit));
        }

        public bool Add(ProjectUnit projectUnit)
        {
            if (!this.Units.First().HasSameGroup(projectUnit)) return false;
            this.Units.Add(projectUnit);
            return true;
        }
    }
}