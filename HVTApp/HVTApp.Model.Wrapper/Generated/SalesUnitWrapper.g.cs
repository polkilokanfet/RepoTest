using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    protected SalesUnitWrapper(SalesUnit model) : base(model) { }

	public static SalesUnitWrapper GetWrapper()
	{
		return GetWrapper(new SalesUnit());
	}

	public static SalesUnitWrapper GetWrapper(SalesUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SalesUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new SalesUnitWrapper(model);
	}



    #region SimpleProperties

    public System.Nullable<System.DateTime> RealizationDate
    {
      get { return GetValue<System.Nullable<System.DateTime>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.DateTime> RealizationDateOriginalValue => GetOriginalValue<System.Nullable<System.DateTime>>(nameof(RealizationDate));
    public bool RealizationDateIsChanged => GetIsChanged(nameof(RealizationDate));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public SalesUnitWrapper ParentSalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.ParentSalesUnit); }
        set
        {
			var oldPropVal = ParentSalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper ParentSalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(ParentSalesUnit)));
    public bool ParentSalesUnitIsChanged => GetIsChanged(nameof(ParentSalesUnit));


	public ProjectWrapper Project 
    {
        get { return ProjectWrapper.GetWrapper(Model.Project); }
        set
        {
			var oldPropVal = Project;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProjectWrapper ProjectOriginalValue => ProjectWrapper.GetWrapper(GetOriginalValue<Project>(nameof(Project)));
    public bool ProjectIsChanged => GetIsChanged(nameof(Project));


	public FacilityWrapper Facility 
    {
        get { return FacilityWrapper.GetWrapper(Model.Facility); }
        set
        {
			var oldPropVal = Facility;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public FacilityWrapper FacilityOriginalValue => FacilityWrapper.GetWrapper(GetOriginalValue<Facility>(nameof(Facility)));
    public bool FacilityIsChanged => GetIsChanged(nameof(Facility));


	public SpecificationWrapper Specification 
    {
        get { return SpecificationWrapper.GetWrapper(Model.Specification); }
        set
        {
			var oldPropVal = Specification;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SpecificationWrapper SpecificationOriginalValue => SpecificationWrapper.GetWrapper(GetOriginalValue<Specification>(nameof(Specification)));
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));


	public SumAndVatWrapper CostSingle 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.CostSingle); }
        set
        {
			var oldPropVal = CostSingle;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper CostSingleOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(CostSingle)));
    public bool CostSingleIsChanged => GetIsChanged(nameof(CostSingle));


	public ProductionUnitWrapper ProductionUnit 
    {
        get { return ProductionUnitWrapper.GetWrapper(Model.ProductionUnit); }
        set
        {
			var oldPropVal = ProductionUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductionUnitWrapper ProductionUnitOriginalValue => ProductionUnitWrapper.GetWrapper(GetOriginalValue<ProductionUnit>(nameof(ProductionUnit)));
    public bool ProductionUnitIsChanged => GetIsChanged(nameof(ProductionUnit));


	public ShipmentUnitWrapper ShipmentUnit 
    {
        get { return ShipmentUnitWrapper.GetWrapper(Model.ShipmentUnit); }
        set
        {
			var oldPropVal = ShipmentUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ShipmentUnitWrapper ShipmentUnitOriginalValue => ShipmentUnitWrapper.GetWrapper(GetOriginalValue<ShipmentUnit>(nameof(ShipmentUnit)));
    public bool ShipmentUnitIsChanged => GetIsChanged(nameof(ShipmentUnit));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> ChildSalesUnits { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentWrapper> PaymentsActual { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(SalesUnit model)
    {

        ParentSalesUnit = SalesUnitWrapper.GetWrapper(model.ParentSalesUnit);

        Project = ProjectWrapper.GetWrapper(model.Project);

        Facility = FacilityWrapper.GetWrapper(model.Facility);

        Specification = SpecificationWrapper.GetWrapper(model.Specification);

        CostSingle = SumAndVatWrapper.GetWrapper(model.CostSingle);

        ProductionUnit = ProductionUnitWrapper.GetWrapper(model.ProductionUnit);

        ShipmentUnit = ShipmentUnitWrapper.GetWrapper(model.ShipmentUnit);

    }

  
    protected override void InitializeCollectionComplexProperties(SalesUnit model)
    {

      if (model.ChildSalesUnits == null) throw new ArgumentException("ChildSalesUnits cannot be null");
      ChildSalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.ChildSalesUnits.Select(e => SalesUnitWrapper.GetWrapper(e)));
      RegisterCollection(ChildSalesUnits, model.ChildSalesUnits);


      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => PaymentConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


      if (model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsPlanned.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsPlanned, model.PaymentsPlanned);


      if (model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentWrapper>(model.PaymentsActual.Select(e => PaymentWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsActual, model.PaymentsActual);


    }

  }
}
