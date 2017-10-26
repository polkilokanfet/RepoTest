using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
	public partial class ExchangeCurrencyRateWrapper : WrapperBase<ExchangeCurrencyRate>
	{
	public ExchangeCurrencyRateWrapper(ExchangeCurrencyRate model) : base(model) { }

	

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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private CurrencyWrapper _fieldFirstCurrency;
	public CurrencyWrapper FirstCurrency 
    {
        get { return _fieldFirstCurrency ; }
        set
        {
            SetComplexValue<Currency, CurrencyWrapper>(_fieldFirstCurrency, value);
            _fieldFirstCurrency  = value;
        }
    }

	private CurrencyWrapper _fieldSecondCurrency;
	public CurrencyWrapper SecondCurrency 
    {
        get { return _fieldSecondCurrency ; }
        set
        {
            SetComplexValue<Currency, CurrencyWrapper>(_fieldSecondCurrency, value);
            _fieldSecondCurrency  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.FirstCurrency != null)
        {
            _fieldFirstCurrency = new CurrencyWrapper(Model.FirstCurrency);
            RegisterComplex(FirstCurrency);
        }

		if (Model.SecondCurrency != null)
        {
            _fieldSecondCurrency = new CurrencyWrapper(Model.SecondCurrency);
            RegisterComplex(SecondCurrency);
        }

    }

	}
}
	