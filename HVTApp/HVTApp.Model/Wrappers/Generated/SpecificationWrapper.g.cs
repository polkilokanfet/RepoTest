using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    public SpecificationWrapper(Specification model) : base(model) { }



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

	private ContractWrapper _fieldContract;
	public ContractWrapper Contract 
    {
        get { return _fieldContract ; }
        set
        {
            SetComplexValue<Contract, ContractWrapper>(_fieldContract, value);
            _fieldContract  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Contract != null)
        {
            _fieldContract = new ContractWrapper(Model.Contract);
            RegisterComplex(Contract);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
      SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(Model.SalesUnits.Select(e => new SalesUnitWrapper(e)));
      RegisterCollection(SalesUnits, Model.SalesUnits);


    }

  }
}
