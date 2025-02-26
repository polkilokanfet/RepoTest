using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnit : WrapperBase<SalesUnit>, IProjectUnit
    {
        #region SimpleProperties

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

        #endregion

        #region Facility

        public string Facility { get; private set; }
        public bool FacilityIsChanged => GetIsChanged(nameof(ProductId));

        public Guid FacilityId => Model.FacilityId;

        public void SetFacility(Facility facility)
        {
            if (SetValue(facility.Id, nameof(SalesUnit.ProductId)) == false) return;
            Facility = facility.ToString();
            RaisePropertyChanged(nameof(Product));
            RaisePropertyChanged(nameof(ProductIsChanged));
        }

        #endregion

        #region Product

        public string Product { get; private set; }
        public bool ProductIsChanged => GetIsChanged(nameof(ProductId));

        public Guid ProductId => Model.ProductId;

        public void SetProduct(Product product)
        {
            if (SetValue(product.Id, nameof(SalesUnit.ProductId)) == false) return;
            Product = product.ToString();
            RaisePropertyChanged(nameof(Product));
            RaisePropertyChanged(nameof(ProductIsChanged));
        }

        #endregion

        #region PaymentConditionSet

        public string PaymentConditionSet { get; private set; }
        public Guid PaymentConditionSetId => Model.PaymentConditionSetId;
        public void SetPaymentConditionSet(PaymentConditionSet paymentConditionSet)
        {
            if (SetValue(paymentConditionSet.Id, nameof(SalesUnit.PaymentConditionSetId)) == false) return;
            PaymentConditionSet = paymentConditionSet.ToString();
            RaisePropertyChanged(nameof(PaymentConditionSet));
        }

        #endregion

        #region Producer

        public string Producer { get; private set; }
        public Guid? ProducerId => Model.ProducerId;

        public void SetProducer(Company producer)
        {
            if (SetValue(producer.Id, nameof(SalesUnit.ProducerId)) == false) return;
            PaymentConditionSet = producer.ToString();
            RaisePropertyChanged(nameof(Producer));
        }

        #endregion

        public void CopyProps(IProjectUnit projectUnit)
        {
            if (projectUnit == null) return;

            Cost = projectUnit.Cost;
            Comment = projectUnit.Comment;
            CostDelivery = projectUnit.CostDelivery;
            DeliveryDateExpected = projectUnit.DeliveryDateExpected;
            SetFacility(new Facility
            {
                Id = projectUnit.FacilityId,
                Name = projectUnit.Facility
            });
        }

        /// <summary>
        /// Для создания по образцу
        /// </summary>
        public ProjectUnit(IProjectUnit projectUnit) : base(new SalesUnit())
        {
            this.CopyProps(projectUnit);
        }

        /// <summary>
        /// Для редактирования
        /// </summary>
        /// <param name="model"></param>
        public ProjectUnit(SalesUnit model) : base(model)
        {
            Facility = model.Facility.ToString();
            Product = model.Product.ToString();
            Producer = model.Producer.ToString();
            PaymentConditionSet = model.PaymentConditionSet.ToString();
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
                if (!Equals(x.ProductId, y.ProductId)) return false;
                if (!Equals(x.FacilityId, y.FacilityId)) return false;
                if (!Equals(x.PaymentConditionSetId, y.PaymentConditionSetId)) return false;
                if (!Equals(x.ProducerId, y.ProducerId)) return false;
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