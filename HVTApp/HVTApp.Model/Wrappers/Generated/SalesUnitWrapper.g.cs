using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class SalesUnitWrapper : WrapperBase<SalesUnit>
  {
    public SalesUnitWrapper() : base(new SalesUnit()) { }
    public SalesUnitWrapper(SalesUnit model) : base(model) { }



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

	public UnitWrapper Unit 
    {
        get { return GetComplexProperty<UnitWrapper, Unit>(Model.Unit); }
        set { SetComplexProperty<UnitWrapper, Unit>(Unit, value); }
    }

    public UnitWrapper UnitOriginalValue { get; private set; }
    public bool UnitIsChanged => GetIsChanged(nameof(Unit));


	public SumAndVatWrapper Cost 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.Cost); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(Cost, value); }
    }

    public SumAndVatWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


	public SpecificationWrapper Specification 
    {
        get { return GetComplexProperty<SpecificationWrapper, Specification>(Model.Specification); }
        set { SetComplexProperty<SpecificationWrapper, Specification>(Specification, value); }
    }

    public SpecificationWrapper SpecificationOriginalValue { get; private set; }
    public bool SpecificationIsChanged => GetIsChanged(nameof(Specification));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentPlanWrapper> PaymentsPlanned { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Unit = GetWrapper<UnitWrapper, Unit>(Model.Unit);

        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(Model.Cost);

        Specification = GetWrapper<SpecificationWrapper, Specification>(Model.Specification);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


      if (Model.PaymentsPlanned == null) throw new ArgumentException("PaymentsPlanned cannot be null");
      PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlanWrapper>(Model.PaymentsPlanned.Select(e => GetWrapper<PaymentPlanWrapper, PaymentPlan>(e)));
      RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);


      if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
      PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => GetWrapper<PaymentActualWrapper, PaymentActual>(e)));
      RegisterCollection(PaymentsActual, Model.PaymentsActual);


    }

  }
}
