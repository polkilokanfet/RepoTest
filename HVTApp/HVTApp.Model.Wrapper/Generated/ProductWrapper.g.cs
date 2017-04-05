using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductWrapper : WrapperBase<Product>
  {
    protected ProductWrapper(Product model) : base(model) { }

	public static ProductWrapper GetWrapper(Product model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public EquipmentWrapper Equipment 
    {
        get { return EquipmentWrapper.GetWrapper(Model.Equipment); }
        set
        {
			var oldPropVal = Equipment;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EquipmentWrapper EquipmentOriginalValue => EquipmentWrapper.GetWrapper(GetOriginalValue<Equipment>(nameof(Equipment)));
    public bool EquipmentIsChanged => GetIsChanged(nameof(Equipment));


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
			var oldPropVal = CostInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CostInfoWrapper CostInfoOriginalValue => CostInfoWrapper.GetWrapper(GetOriginalValue<CostInfo>(nameof(CostInfo)));
    public bool CostInfoIsChanged => GetIsChanged(nameof(CostInfo));


	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return PaymentsInfoWrapper.GetWrapper(Model.PaymentsInfo); }
        set
        {
			var oldPropVal = PaymentsInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public PaymentsInfoWrapper PaymentsInfoOriginalValue => PaymentsInfoWrapper.GetWrapper(GetOriginalValue<PaymentsInfo>(nameof(PaymentsInfo)));
    public bool PaymentsInfoIsChanged => GetIsChanged(nameof(PaymentsInfo));


    #endregion

    protected override void InitializeComplexProperties(Product model)
    {

        Equipment = EquipmentWrapper.GetWrapper(model.Equipment);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  }
}
