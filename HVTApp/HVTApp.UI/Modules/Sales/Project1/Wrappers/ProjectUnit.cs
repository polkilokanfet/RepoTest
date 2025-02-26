using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
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

        #region ComplexProperties

        public FacilityEmptyWrapper Facility
        {
            get => GetWrapper<FacilityEmptyWrapper>();
            set => SetComplexValue<Facility, FacilityEmptyWrapper>(Facility, value);
        }

        public ProductEmptyWrapper Product
        {
            get => GetWrapper<ProductEmptyWrapper>();
            set => SetComplexValue<Product, ProductEmptyWrapper>(Product, value);
        }

        public PaymentConditionSetEmptyWrapper PaymentConditionSet
        {
            get => GetWrapper<PaymentConditionSetEmptyWrapper>();
            set => SetComplexValue<PaymentConditionSet, PaymentConditionSetEmptyWrapper>(PaymentConditionSet, value);
        }

        public CompanyEmptyWrapper Producer
        {
            get => GetWrapper<CompanyEmptyWrapper>();
            set => SetComplexValue<Company, CompanyEmptyWrapper>(Producer, value);
        }

        #endregion

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
        }

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(Facility), Model.Facility == null ? null : new FacilityEmptyWrapper(Model.Facility));
            InitializeComplexProperty(nameof(Product), Model.Product == null ? null : new ProductEmptyWrapper(Model.Product));
            InitializeComplexProperty(nameof(PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetEmptyWrapper(Model.PaymentConditionSet));
            InitializeComplexProperty(nameof(Producer), Model.Producer == null ? null : new CompanyEmptyWrapper(Model.Producer));
        }

        public void CopyProps(IProjectUnit projectUnit)
        {
            if (projectUnit == null) return;

            Cost = projectUnit.Cost;
            Comment = projectUnit.Comment;
            CostDelivery = projectUnit.CostDelivery;
            DeliveryDateExpected = projectUnit.DeliveryDateExpected;

            Facility = projectUnit.Facility;
            Product = projectUnit.Product;
            PaymentConditionSet = projectUnit.PaymentConditionSet;
            Producer = projectUnit.Producer;
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
                if (!Equals(x.Product, y.Product)) return false;
                if (!Equals(x.Facility, y.Facility)) return false;
                if (!Equals(x.PaymentConditionSet, y.PaymentConditionSet)) return false;
                if (!Equals(x.Producer, y.Producer)) return false;
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