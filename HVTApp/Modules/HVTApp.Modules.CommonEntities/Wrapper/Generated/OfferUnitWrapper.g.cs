using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class OfferUnitWrapper : WrapperBase<OfferUnit>
	{
	public OfferUnitWrapper(OfferUnit model) : base(model) { }

	
    #region SimpleProperties
    public System.Guid OfferId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid OfferIdOriginalValue => GetOriginalValue<System.Guid>(nameof(OfferId));
    public bool OfferIdIsChanged => GetIsChanged(nameof(OfferId));

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

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<ProjectUnitWrapper>(nameof(ProjectUnit), Model.ProjectUnit == null ? null : new ProjectUnitWrapper(Model.ProjectUnit));

        InitializeComplexProperty<FacilityWrapper>(nameof(Facility), Model.Facility == null ? null : new FacilityWrapper(Model.Facility));

        InitializeComplexProperty<ProductWrapper>(nameof(Product), Model.Product == null ? null : new ProductWrapper(Model.Product));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => new PaymentConditionWrapper(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);

    }
	}
}
	