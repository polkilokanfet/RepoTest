using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    public SpecificationWrapper() : base(new Specification()) { }
    public SpecificationWrapper(Specification model) : base(model) { }

//	public static SpecificationWrapper GetWrapper()
//	{
//		return GetWrapper(new Specification());
//	}
//
//	public static SpecificationWrapper GetWrapper(Specification model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (SpecificationWrapper)Repository.ExistsWrappers[model];
//
//		return new SpecificationWrapper(model);
//	}


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

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private ContractWrapper _fieldContract;
	public ContractWrapper Contract 
    {
        get { return _fieldContract; }
        set
        {
			SetComplexProperty<ContractWrapper, Contract>(_fieldContract, value);
			_fieldContract = value;
        }
    }
    public ContractWrapper ContractOriginalValue { get; private set; }
    public bool ContractIsChanged => GetIsChanged(nameof(Contract));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<SalesUnitWrapper> SalesUnits { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Specification model)
    {
        Contract = GetWrapper<ContractWrapper, Contract>(model.Contract);
    }
  
    protected override void InitializeCollectionComplexProperties(Specification model)
    {
      if (model.SalesUnits == null) throw new ArgumentException("SalesUnits cannot be null");
      SalesUnits = new ValidatableChangeTrackingCollection<SalesUnitWrapper>(model.SalesUnits.Select(e => GetWrapper<SalesUnitWrapper, SalesUnit>(e)));
      RegisterCollection(SalesUnits, model.SalesUnits);

    }
  }
}
