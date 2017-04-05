using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    protected SpecificationWrapper(Specification model) : base(model) { }

	public static SpecificationWrapper GetWrapper(Specification model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SpecificationWrapper)Repository.ModelWrapperDictionary[model];

		return new SpecificationWrapper(model);
	}



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

	public ContractWrapper Contract 
    {
        get { return ContractWrapper.GetWrapper(Model.Contract); }
        set
        {
			var oldPropVal = Contract;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ContractWrapper ContractOriginalValue => ContractWrapper.GetWrapper(GetOriginalValue<Contract>(nameof(Contract)));
    public bool ContractIsChanged => GetIsChanged(nameof(Contract));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductsMainGroupWrapper> SalesGroups { get; private set; }


    public IValidatableChangeTrackingCollection<PaymentsConditionWrapper> PaymentsConditions { get; private set; }


    #endregion


    #region GetProperties

    public System.Double Sum => GetValue<System.Double>(); 


    public System.Double SumWithVat => GetValue<System.Double>(); 


    #endregion

    protected override void InitializeComplexProperties(Specification model)
    {

        Contract = ContractWrapper.GetWrapper(model.Contract);

    }

  
    protected override void InitializeCollectionComplexProperties(Specification model)
    {

      if (model.SalesGroups == null) throw new ArgumentException("SalesGroups cannot be null");
      SalesGroups = new ValidatableChangeTrackingCollection<ProductsMainGroupWrapper>(model.SalesGroups.Select(e => ProductsMainGroupWrapper.GetWrapper(e)));
      RegisterCollection(SalesGroups, model.SalesGroups);


      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentsConditionWrapper>(model.PaymentsConditions.Select(e => PaymentsConditionWrapper.GetWrapper(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);


    }

  }
}
