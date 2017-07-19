using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ExchangeCurrencyRateWrapper : WrapperBase<ExchangeCurrencyRate>
  {
    private ExchangeCurrencyRateWrapper(IGetWrapper getWrapper) : base(new ExchangeCurrencyRate(), getWrapper) { }
    private ExchangeCurrencyRateWrapper(ExchangeCurrencyRate model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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
        get { return GetComplexProperty<CurrencyWrapper, Currency>(Model.FirstCurrency); }
        set { SetComplexProperty<CurrencyWrapper, Currency>(FirstCurrency, value); }
    }

    public CurrencyWrapper FirstCurrencyOriginalValue { get; private set; }
    public bool FirstCurrencyIsChanged => GetIsChanged(nameof(FirstCurrency));


	public CurrencyWrapper SecondCurrency 
    {
        get { return GetComplexProperty<CurrencyWrapper, Currency>(Model.SecondCurrency); }
        set { SetComplexProperty<CurrencyWrapper, Currency>(SecondCurrency, value); }
    }

    public CurrencyWrapper SecondCurrencyOriginalValue { get; private set; }
    public bool SecondCurrencyIsChanged => GetIsChanged(nameof(SecondCurrency));


    #endregion

    public override void InitializeComplexProperties()
    {

        FirstCurrency = GetWrapper<CurrencyWrapper, Currency>(Model.FirstCurrency);

        SecondCurrency = GetWrapper<CurrencyWrapper, Currency>(Model.SecondCurrency);

    }

  }
}
