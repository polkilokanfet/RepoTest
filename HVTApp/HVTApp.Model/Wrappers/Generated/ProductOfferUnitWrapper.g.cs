using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductOfferUnitWrapper : WrapperBase<ProductOfferUnit>
  {
    private ProductOfferUnitWrapper() : base(new ProductOfferUnit()) { }
    private ProductOfferUnitWrapper(ProductOfferUnit model) : base(model) { }



    #region SimpleProperties

    public System.Int32 ProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
    public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProductComplexUnitWrapper ProductComplexUnit 
    {
        get { return GetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit); }
        set { SetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(ProductComplexUnit, value); }
    }

    public ProductComplexUnitWrapper ProductComplexUnitOriginalValue { get; private set; }
    public bool ProductComplexUnitIsChanged => GetIsChanged(nameof(ProductComplexUnit));


	public OfferWrapper Offer 
    {
        get { return GetComplexProperty<OfferWrapper, Offer>(Model.Offer); }
        set { SetComplexProperty<OfferWrapper, Offer>(Offer, value); }
    }

    public OfferWrapper OfferOriginalValue { get; private set; }
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public SumAndVatWrapper Cost 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.Cost); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(Cost, value); }
    }

    public SumAndVatWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        ProductComplexUnit = GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit);

        Offer = GetWrapper<OfferWrapper, Offer>(Model.Offer);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(Model.Cost);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

  }
}
