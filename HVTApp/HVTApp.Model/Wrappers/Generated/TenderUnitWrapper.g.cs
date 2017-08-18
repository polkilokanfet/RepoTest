using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    private TenderUnitWrapper(IGetWrapper getWrapper) : base(new TenderUnit(), getWrapper) { }
    private TenderUnitWrapper(TenderUnit model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.Guid ProjectUnitId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid ProjectUnitIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ProjectUnitId));
    public bool ProjectUnitIdIsChanged => GetIsChanged(nameof(ProjectUnitId));


    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    public System.Nullable<System.Guid> ProducerWinnerId
    {
      get { return GetValue<System.Nullable<System.Guid>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Guid> ProducerWinnerIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(ProducerWinnerId));
    public bool ProducerWinnerIdIsChanged => GetIsChanged(nameof(ProducerWinnerId));


    public System.DateTime DeliveryDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DeliveryDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(DeliveryDate));
    public bool DeliveryDateIsChanged => GetIsChanged(nameof(DeliveryDate));


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


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public TenderWrapper Tender 
    {
        get { return GetComplexProperty<TenderWrapper, Tender>(Model.Tender); }
        set { SetComplexProperty<TenderWrapper, Tender>(Tender, value); }
    }

    public TenderWrapper TenderOriginalValue { get; private set; }
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));


	public CompanyWrapper ProducerWinner 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.ProducerWinner); }
        set { SetComplexProperty<CompanyWrapper, Company>(ProducerWinner, value); }
    }

    public CompanyWrapper ProducerWinnerOriginalValue { get; private set; }
    public bool ProducerWinnerIsChanged => GetIsChanged(nameof(ProducerWinner));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<OfferUnitWrapper> OfferUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        ProjectUnit = GetWrapper<ProjectUnitWrapper, ProjectUnit>(Model.ProjectUnit);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Tender = GetWrapper<TenderWrapper, Tender>(Model.Tender);

        ProducerWinner = GetWrapper<CompanyWrapper, Company>(Model.ProducerWinner);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


      if (Model.OfferUnits == null) throw new ArgumentException("OfferUnits cannot be null");
      OfferUnits = new ValidatableChangeTrackingCollection<OfferUnitWrapper>(Model.OfferUnits.Select(e => GetWrapper<OfferUnitWrapper, OfferUnit>(e)));
      RegisterCollection(OfferUnits, Model.OfferUnits);


    }

  }
}
