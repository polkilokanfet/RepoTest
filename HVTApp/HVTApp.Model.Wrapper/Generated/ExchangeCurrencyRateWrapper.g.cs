using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ExchangeCurrencyRateWrapper : WrapperBase<ExchangeCurrencyRate>
  {
    public ExchangeCurrencyRateWrapper() : base(new ExchangeCurrencyRate()) { }
    public ExchangeCurrencyRateWrapper(ExchangeCurrencyRate model) : base(model) { }

//	public static ExchangeCurrencyRateWrapper GetWrapper()
//	{
//		return GetWrapper(new ExchangeCurrencyRate());
//	}
//
//	public static ExchangeCurrencyRateWrapper GetWrapper(ExchangeCurrencyRate model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (ExchangeCurrencyRateWrapper)Repository.ExistsWrappers[model];
//
//		return new ExchangeCurrencyRateWrapper(model);
//	}


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
	private CurrencyWrapper _fieldFirstCurrency;
	public CurrencyWrapper FirstCurrency 
    {
        get { return _fieldFirstCurrency; }
        set
        {
			SetComplexProperty<CurrencyWrapper, Currency>(_fieldFirstCurrency, value);
			_fieldFirstCurrency = value;
        }
    }
    public CurrencyWrapper FirstCurrencyOriginalValue { get; private set; }
    public bool FirstCurrencyIsChanged => GetIsChanged(nameof(FirstCurrency));

	private CurrencyWrapper _fieldSecondCurrency;
	public CurrencyWrapper SecondCurrency 
    {
        get { return _fieldSecondCurrency; }
        set
        {
			SetComplexProperty<CurrencyWrapper, Currency>(_fieldSecondCurrency, value);
			_fieldSecondCurrency = value;
        }
    }
    public CurrencyWrapper SecondCurrencyOriginalValue { get; private set; }
    public bool SecondCurrencyIsChanged => GetIsChanged(nameof(SecondCurrency));

    #endregion
    protected override void InitializeComplexProperties(ExchangeCurrencyRate model)
    {
        FirstCurrency = GetWrapper<CurrencyWrapper, Currency>(model.FirstCurrency);
        SecondCurrency = GetWrapper<CurrencyWrapper, Currency>(model.SecondCurrency);
    }
  }
}
