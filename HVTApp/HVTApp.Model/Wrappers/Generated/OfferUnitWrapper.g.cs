using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
  {
    private OfferUnitWrapper(IGetWrapper getWrapper) : base(new OfferUnit(), getWrapper) { }
    private OfferUnitWrapper(OfferUnit model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.Nullable<System.Guid> ProjectUnitId
    {
      get { return GetValue<System.Nullable<System.Guid>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Guid> ProjectUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProjectUnitId));
    public bool ProjectUnitIdIsChanged => GetIsChanged(nameof(ProjectUnitId));


    public System.Nullable<System.Guid> TenderUnitId
    {
      get { return GetValue<System.Nullable<System.Guid>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Guid> TenderUnitIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(TenderUnitId));
    public bool TenderUnitIdIsChanged => GetIsChanged(nameof(TenderUnitId));


    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.Int32 ProductionTerm
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 ProductionTermOriginalValue => GetOriginalValue<System.Int32>(nameof(ProductionTerm));
    public bool ProductionTermIsChanged => GetIsChanged(nameof(ProductionTerm));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ProjectUnitWrapper ProjectUnit 
    {
        get { return GetComplexProperty<ProjectUnitWrapper, ProjectUnit>(Model.ProjectUnit); }
        set { SetComplexProperty<ProjectUnitWrapper, ProjectUnit>(ProjectUnit, value); }
    }

    public ProjectUnitWrapper ProjectUnitOriginalValue { get; private set; }
    public bool ProjectUnitIsChanged => GetIsChanged(nameof(ProjectUnit));


	public TenderUnitWrapper TenderUnit 
    {
        get { return GetComplexProperty<TenderUnitWrapper, TenderUnit>(Model.TenderUnit); }
        set { SetComplexProperty<TenderUnitWrapper, TenderUnit>(TenderUnit, value); }
    }

    public TenderUnitWrapper TenderUnitOriginalValue { get; private set; }
    public bool TenderUnitIsChanged => GetIsChanged(nameof(TenderUnit));


    public SalesUnitWrapper SalesUnitOriginalValue { get; private set; }
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public OfferWrapper Offer 
    {
        get { return GetComplexProperty<OfferWrapper, Offer>(Model.Offer); }
        set { SetComplexProperty<OfferWrapper, Offer>(Offer, value); }
    }

    public OfferWrapper OfferOriginalValue { get; private set; }
    public bool OfferIsChanged => GetIsChanged(nameof(Offer));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        ProjectUnit = GetWrapper<ProjectUnitWrapper, ProjectUnit>(Model.ProjectUnit);

        TenderUnit = GetWrapper<TenderUnitWrapper, TenderUnit>(Model.TenderUnit);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Offer = GetWrapper<OfferWrapper, Offer>(Model.Offer);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

  }
}
