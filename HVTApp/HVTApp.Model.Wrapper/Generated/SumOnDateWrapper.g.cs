using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class SumOnDateWrapper : WrapperBase<SumOnDate>
  {
    protected SumOnDateWrapper(SumOnDate model) : base(model) { }

	public static SumOnDateWrapper GetWrapper()
	{
		return GetWrapper(new SumOnDate());
	}

	public static SumOnDateWrapper GetWrapper(SumOnDate model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (SumOnDateWrapper)Repository.ModelWrapperDictionary[model];

		return new SumOnDateWrapper(model);
	}



    #region SimpleProperties

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

	public SumAndVatWrapper SumAndVat 
    {
        get { return SumAndVatWrapper.GetWrapper(Model.SumAndVat); }
        set
        {
			var oldPropVal = SumAndVat;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public SumAndVatWrapper SumAndVatOriginalValue => SumAndVatWrapper.GetWrapper(GetOriginalValue<SumAndVat>(nameof(SumAndVat)));
    public bool SumAndVatIsChanged => GetIsChanged(nameof(SumAndVat));


    #endregion

    protected override void InitializeComplexProperties(SumOnDate model)
    {

        SumAndVat = SumAndVatWrapper.GetWrapper(model.SumAndVat);

    }

  }
}
