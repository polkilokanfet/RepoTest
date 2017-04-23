using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class StandartDeliveryPeriodWrapper : WrapperBase<StandartDeliveryPeriod>
  {
    public StandartDeliveryPeriodWrapper() : base(new StandartDeliveryPeriod()) { }
    public StandartDeliveryPeriodWrapper(StandartDeliveryPeriod model) : base(model) { }

//	public static StandartDeliveryPeriodWrapper GetWrapper()
//	{
//		return GetWrapper(new StandartDeliveryPeriod());
//	}
//
//	public static StandartDeliveryPeriodWrapper GetWrapper(StandartDeliveryPeriod model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (StandartDeliveryPeriodWrapper)Repository.ExistsWrappers[model];
//
//		return new StandartDeliveryPeriodWrapper(model);
//	}


    #region SimpleProperties
    public System.Int32 DeliveryPeriod
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 DeliveryPeriodOriginalValue => GetOriginalValue<System.Int32>(nameof(DeliveryPeriod));
    public bool DeliveryPeriodIsChanged => GetIsChanged(nameof(DeliveryPeriod));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private LocalityWrapper _fieldLocality;
	public LocalityWrapper Locality 
    {
        get { return _fieldLocality; }
        set
        {
			SetComplexProperty<LocalityWrapper, Locality>(_fieldLocality, value);
			_fieldLocality = value;
        }
    }
    public LocalityWrapper LocalityOriginalValue { get; private set; }
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));

    #endregion
    protected override void InitializeComplexProperties(StandartDeliveryPeriod model)
    {
        Locality = GetWrapper<LocalityWrapper, Locality>(model.Locality);
    }
  }
}
