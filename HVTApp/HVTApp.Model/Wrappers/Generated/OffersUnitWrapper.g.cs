using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OffersUnitWrapper : WrapperBase<OffersUnit>
  {
    public OffersUnitWrapper() : base(new OffersUnit(), new Dictionary<IBaseEntity, object>()) { }
    public OffersUnitWrapper(OffersUnit model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public OffersUnitWrapper(OffersUnit model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



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

	public UnitWrapper Unit 
    {
        get { return GetComplexProperty<UnitWrapper, Unit>(Model.Unit); }
        set { SetComplexProperty<UnitWrapper, Unit>(Unit, value); }
    }

    public UnitWrapper UnitOriginalValue { get; private set; }
    public bool UnitIsChanged => GetIsChanged(nameof(Unit));


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

    protected override void InitializeComplexProperties(OffersUnit model)
    {

        Unit = GetWrapper<UnitWrapper, Unit>(model.Unit);

        Offer = GetWrapper<OfferWrapper, Offer>(model.Offer);

        Product = GetWrapper<ProductWrapper, Product>(model.Product);

        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(model.Cost);

    }

  
    protected override void InitializeCollectionComplexProperties(OffersUnit model)
    {

      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


    }

  }
}
