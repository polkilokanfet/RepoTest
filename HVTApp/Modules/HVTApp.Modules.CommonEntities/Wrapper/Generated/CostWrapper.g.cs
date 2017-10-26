using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class CostWrapper : WrapperBase<Cost>
	{
	public CostWrapper(Cost model) : base(model) { }

	

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

	private CurrencyWrapper _fieldCurrency;
	public CurrencyWrapper Currency 
    {
        get { return _fieldCurrency ; }
        set
        {
            SetComplexValue<Currency, CurrencyWrapper>(_fieldCurrency, value);
            _fieldCurrency  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Currency != null)
        {
            _fieldCurrency = new CurrencyWrapper(Model.Currency);
            RegisterComplex(Currency);
        }

    }

	}
}
	