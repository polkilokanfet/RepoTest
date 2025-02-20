using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnit : WrapperBase<SalesUnit>, IProjectUnit
    {
        public double Cost
        {
            get => Model.Cost;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public double? CostDelivery
        {
            get => Model.CostDelivery;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public DateTime DeliveryDateExpected
        {
            get => Model.DeliveryDateExpected;
            set => SetValue(value);
        }

        public bool IsRemoved
        {
            get => Model.IsRemoved;
            set => SetValue(value);
        }

        public int ProductionTerm
        {
            get => Model.ProductionTerm;
            set
            {
                if (value < 0) return;
                SetValue(value);
            }
        }

        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }

        #region Facility

        private Facility _facility;
        public Facility Facility
        {
            get => _facility;
            set => SetProperty(ref _facility, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.FacilityId));
            });
        }

        #endregion

        #region Product

        private Product _product;
        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.ProductId));
            });
        }

        private Project _project;
        public Project Project
        {
            get => _project;
            set => SetProperty(ref _project, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.ProjectId));
            });
        }


        #endregion

        #region PaymentConditionSet

        private PaymentConditionSet _paymentConditionSet;
        public PaymentConditionSet PaymentConditionSet
        {
            get => _paymentConditionSet;
            set => SetProperty(ref _paymentConditionSet, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.PaymentConditionSetId));
            });
        }

        #endregion

        #region Producer

        private Company _producer;
        public Company Producer
        {
            get => _producer;
            set => SetProperty(ref _producer, value, () =>
            {
                this.SetValue(value.Id, nameof(SalesUnit.ProducerId));
            });
        }

        #endregion

        public ProjectUnit(SalesUnit model) : base(model)
        {
        }

        public bool HasSameGroup(SalesUnit other)
        {
            return (new ProjectUnitComparer()).Equals(this, new ProjectUnit(other));
        }

        public bool HasSameGroup(ProjectUnit other)
        {
            return (new ProjectUnitComparer()).Equals(this, other);
        }

        public class ProjectUnitComparer : IEqualityComparer<IProjectUnit>
        {
            public virtual bool Equals(IProjectUnit x, IProjectUnit y)
            {
                if (x == null) throw new ArgumentNullException(nameof(x));
                if (y == null) throw new ArgumentNullException(nameof(y));

                if (!Equals(x.Cost, y.Cost)) return false;
                if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
                if (!Equals(x.Product.Id, y.Product.Id)) return false;
                if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
                if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
                if (!Equals(x.CostDelivery, y.CostDelivery)) return false;
                if (!Equals(x.Comment, y.Comment)) return false;
                if (!Equals(x.DeliveryDateExpected, y.DeliveryDateExpected)) return false;

                //var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
                //var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

                //if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
                //if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

                return true;
            }

            public int GetHashCode(IProjectUnit obj)
            {
                return 0;
            }
        }
    }
}