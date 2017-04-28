using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class SumAndVatWrapper : WrapperBase<SumAndVat>
  {
    public SumAndVatWrapper() : base(new SumAndVat()) { }
    public SumAndVatWrapper(SumAndVat model) : base(model) { }
    public SumAndVatWrapper(SumAndVat model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public SumAndVatWrapper(SumAndVat model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



    #region SimpleProperties

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.Double Vat
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double VatOriginalValue => GetOriginalValue<System.Double>(nameof(Vat));
    public bool VatIsChanged => GetIsChanged(nameof(Vat));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public CurrencyWrapper Currency 
    {
        get { return GetComplexProperty<CurrencyWrapper, Currency>(Model.Currency); }
        set { SetComplexProperty<CurrencyWrapper, Currency>(Currency, value); }
    }

    public CurrencyWrapper CurrencyOriginalValue { get; private set; }
    public bool CurrencyIsChanged => GetIsChanged(nameof(Currency));


    #endregion

    protected override void InitializeComplexProperties(SumAndVat model)
    {

        Currency = GetWrapper<CurrencyWrapper, Currency>(model.Currency);

    }

  }
}
