using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class CostWrapper : WrapperBase<Cost>
  {
    private CostWrapper() : base(new Cost()) { }
    private CostWrapper(Cost model) : base(model) { }



    #region SimpleProperties

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
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

    public override void InitializeComplexProperties()
    {

        Currency = GetWrapper<CurrencyWrapper, Currency>(Model.Currency);

    }

  }
}
