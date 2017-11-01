using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
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
	public CurrencyWrapper FirstCurrency 
    {
        get { return GetWrapper<CurrencyWrapper>(); }
        set { SetComplexValue<Currency, CurrencyWrapper>(FirstCurrency, value); }
    }

	public CurrencyWrapper SecondCurrency 
    {
        get { return GetWrapper<CurrencyWrapper>(); }
        set { SetComplexValue<Currency, CurrencyWrapper>(SecondCurrency, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<CurrencyWrapper>(nameof(FirstCurrency), Model.FirstCurrency == null ? null : new CurrencyWrapper(Model.FirstCurrency));

        InitializeComplexProperty<CurrencyWrapper>(nameof(SecondCurrency), Model.SecondCurrency == null ? null : new CurrencyWrapper(Model.SecondCurrency));

    }
	}
}
	