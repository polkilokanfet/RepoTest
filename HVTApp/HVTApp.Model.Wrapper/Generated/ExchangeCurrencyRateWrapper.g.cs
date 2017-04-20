using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ExchangeCurrencyRateWrapper : WrapperBase<ExchangeCurrencyRate>
  {
    protected ExchangeCurrencyRateWrapper(ExchangeCurrencyRate model) : base(model) { }

	public static ExchangeCurrencyRateWrapper GetWrapper()
	{
		return GetWrapper(new ExchangeCurrencyRate());
	}

	public static ExchangeCurrencyRateWrapper GetWrapper(ExchangeCurrencyRate model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ExchangeCurrencyRateWrapper)Repository.ModelWrapperDictionary[model];

		return new ExchangeCurrencyRateWrapper(model);
	}



    #region SimpleProperties

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Double FirstCurrencyValue
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double FirstCurrencyValueOriginalValue => GetOriginalValue<System.Double>(nameof(FirstCurrencyValue));
    public bool FirstCurrencyValueIsChanged => GetIsChanged(nameof(FirstCurrencyValue));


    public System.Double SecondCurrencyValue
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SecondCurrencyValueOriginalValue => GetOriginalValue<System.Double>(nameof(SecondCurrencyValue));
    public bool SecondCurrencyValueIsChanged => GetIsChanged(nameof(SecondCurrencyValue));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public CurrencyWrapper FirstCurrency 
    {
        get { return CurrencyWrapper.GetWrapper(Model.FirstCurrency); }
        set
        {
			var oldPropVal = FirstCurrency;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CurrencyWrapper FirstCurrencyOriginalValue => CurrencyWrapper.GetWrapper(GetOriginalValue<Currency>(nameof(FirstCurrency)));
    public bool FirstCurrencyIsChanged => GetIsChanged(nameof(FirstCurrency));


	public CurrencyWrapper SecondCurrency 
    {
        get { return CurrencyWrapper.GetWrapper(Model.SecondCurrency); }
        set
        {
			var oldPropVal = SecondCurrency;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CurrencyWrapper SecondCurrencyOriginalValue => CurrencyWrapper.GetWrapper(GetOriginalValue<Currency>(nameof(SecondCurrency)));
    public bool SecondCurrencyIsChanged => GetIsChanged(nameof(SecondCurrency));


    #endregion

    protected override void InitializeComplexProperties(ExchangeCurrencyRate model)
    {

        FirstCurrency = CurrencyWrapper.GetWrapper(model.FirstCurrency);

        SecondCurrency = CurrencyWrapper.GetWrapper(model.SecondCurrency);

    }

  }
}
