using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    private SpecificationWrapper() : base(new Specification()) { }
    private SpecificationWrapper(Specification model) : base(model) { }



    #region SimpleProperties

    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));


    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Double Vat
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
    public bool VatIsChanged => GetIsChanged(nameof(Vat));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ContractWrapper Contract 
    {
        get { return GetComplexProperty<ContractWrapper, Contract>(Model.Contract); }
        set { SetComplexProperty<ContractWrapper, Contract>(Contract, value); }
    }

    public ContractWrapper ContractOriginalValue { get; private set; }
    public bool ContractIsChanged => GetIsChanged(nameof(Contract));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Contract = GetWrapper<ContractWrapper, Contract>(Model.Contract);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
      SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => GetWrapper<SalesUnitWrapper, SalesUnit>(e)));
      RegisterCollection(SalesUnits, Model.SalesUnits);


    }

  }
}
