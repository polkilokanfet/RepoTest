using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
	{
	public TenderUnitWrapper(TenderUnit model) : base(model) { }

	
    #region SimpleProperties
    public System.Guid TenderId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid TenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(TenderId));
    public bool TenderIdIsChanged => GetIsChanged(nameof(TenderId));

    public System.Double Cost
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double CostOriginalValue => GetOriginalValue<System.Double>(nameof(Cost));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));

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
        get { return GetWrapper<ProjectUnitWrapper>(); }
        set { SetComplexValue<ProjectUnit, ProjectUnitWrapper>(ProjectUnit, value); }
    }

	public FacilityWrapper Facility 
    {
        get { return GetWrapper<FacilityWrapper>(); }
        set { SetComplexValue<Facility, FacilityWrapper>(Facility, value); }
    }

	public ProductWrapper Product 
    {
        get { return GetWrapper<ProductWrapper>(); }
        set { SetComplexValue<Product, ProductWrapper>(Product, value); }
    }

	public CompanyWrapper ProducerWinner 
    {
        get { return GetWrapper<CompanyWrapper>(); }
        set { SetComplexValue<Company, CompanyWrapper>(ProducerWinner, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<ProjectUnitWrapper>(nameof(ProjectUnit), Model.ProjectUnit == null ? null : new ProjectUnitWrapper(Model.ProjectUnit));

        InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

        InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

        InitializeComplexProperty<CompanyWrapper>(nameof(ProducerWinner), Model.ProducerWinner == null ? null : new CompanyWrapper(Model.ProducerWinner));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

    }
	}
}
	