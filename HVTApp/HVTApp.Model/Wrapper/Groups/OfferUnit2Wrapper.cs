using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public class OfferUnit2Wrapper : WrapperBase<OfferUnit>, IWrapperGroup<OfferUnit>
    {

        public OfferUnit2Wrapper(OfferUnit model) : base(model)
        {
        }

        #region SimpleProperties

        public double Cost
        {
            get { return Model.Cost; }
            set { SetValue(value); }
        }
        public double CostOriginalValue => GetOriginalValue<double>(nameof(Cost));
        public bool CostIsChanged => GetIsChanged(nameof(Cost));

        public int ProductionTerm
        {
            get { return Model.ProductionTerm; }
            set { SetValue(value); }
        }
        public int ProductionTermOriginalValue => GetOriginalValue<int>(nameof(ProductionTerm));
        public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));

        public double? CostDelivery
        {
            get { return Model.CostDelivery; }
            set { SetValue(value); }
        }
        public double? CostDeliveryOriginalValue => GetOriginalValue<double?>(nameof(CostDelivery));
        public bool CostDeliveryIsChanged => GetIsChanged(nameof(CostDelivery));

        #endregion

        #region ComplexProperties


        public OfferWrapper Offer
        {
            get { return GetWrapper<OfferWrapper>(); }
            set { SetComplexValue<Offer, OfferWrapper>(Offer, value); }
        }


        public FacilitySimpleWrapper Facility
        {
            get { return GetWrapper<FacilitySimpleWrapper>(); }
            set { SetComplexValue<Facility, FacilitySimpleWrapper>(Facility, value); }
        }

        public ProductSimpleWrapper Product
        {
            get { return GetWrapper<ProductSimpleWrapper>(); }
            set { SetComplexValue<Product, ProductSimpleWrapper>(Product, value); }
        }

        public PaymentConditionSetSimpleWrapper PaymentConditionSet
        {
            get { return GetWrapper<PaymentConditionSetSimpleWrapper>(); }
            set { SetComplexValue<PaymentConditionSet, PaymentConditionSetSimpleWrapper>(PaymentConditionSet, value); }
        }

        public ProductType ProductType => Product.Model.ProductType;



        #endregion

        #region CollectionProperties

        public IValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper> ProductsIncluded { get; private set; }

        #endregion


        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty<OfferWrapper>(nameof(Offer), Model.Offer == null ? null : new OfferWrapper(Model.Offer));

            InitializeComplexProperty(nameof(this.Facility), Model.Facility == null ? null : new FacilitySimpleWrapper(Model.Facility));
            InitializeComplexProperty(nameof(this.Product), Model.Product == null ? null : new ProductSimpleWrapper(Model.Product));
            InitializeComplexProperty(nameof(this.PaymentConditionSet), Model.PaymentConditionSet == null ? null : new PaymentConditionSetSimpleWrapper(Model.PaymentConditionSet));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.ProductsIncluded == null) throw new ArgumentException("ProductsIncluded cannot be null");
            ProductsIncluded = new ValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper>(Model.ProductsIncluded.Select(e => new ProductIncludedSimpleWrapper(e)));
            RegisterCollection(ProductsIncluded, Model.ProductsIncluded);
        }
    }
}