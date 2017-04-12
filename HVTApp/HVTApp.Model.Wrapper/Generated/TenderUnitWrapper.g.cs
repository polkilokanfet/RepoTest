using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderUnitWrapper : WrapperBase<TenderUnit>
  {
    protected TenderUnitWrapper(TenderUnit model) : base(model) { }

	public static TenderUnitWrapper GetWrapper()
	{
		return GetWrapper(new TenderUnit());
	}

	public static TenderUnitWrapper GetWrapper(TenderUnit model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TenderUnitWrapper)Repository.ModelWrapperDictionary[model];

		return new TenderUnitWrapper(model);
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

	public SalesUnitWrapper SalesUnit 
    {
        get { return SalesUnitWrapper.GetWrapper(Model.SalesUnit); }
        set
        {
			var oldPropVal = SalesUnit;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SalesUnitWrapper SalesUnitOriginalValue => SalesUnitWrapper.GetWrapper(GetOriginalValue<SalesUnit>(nameof(SalesUnit)));
    public bool SalesUnitIsChanged => GetIsChanged(nameof(SalesUnit));


	public SumAndVatWrapper Cost 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.Cost); }
        set
        {
			var oldPropVal = Cost;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper CostOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(Cost)));
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion

    protected override void InitializeComplexProperties(TenderUnit model)
    {

        SalesUnit = SalesUnitWrapper.GetWrapper(model.SalesUnit);

        Cost = SumAndVatWrapper.GetWrapper(model.Cost);

    }

  }
}
