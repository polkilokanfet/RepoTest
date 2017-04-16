using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class StandartDeliveryPeriodWrapper : WrapperBase<StandartDeliveryPeriod>
  {
    protected StandartDeliveryPeriodWrapper(StandartDeliveryPeriod model) : base(model) { }

	public static StandartDeliveryPeriodWrapper GetWrapper()
	{
		return GetWrapper(new StandartDeliveryPeriod());
	}

	public static StandartDeliveryPeriodWrapper GetWrapper(StandartDeliveryPeriod model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (StandartDeliveryPeriodWrapper)Repository.ModelWrapperDictionary[model];

		return new StandartDeliveryPeriodWrapper(model);
	}



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

	public LocalityWrapper Locality 
    {
        get { return LocalityWrapper.GetWrapper(Model.Locality); }
        set
        {
			var oldPropVal = Locality;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public LocalityWrapper LocalityOriginalValue => LocalityWrapper.GetWrapper(GetOriginalValue<Locality>(nameof(Locality)));
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));


    #endregion

    protected override void InitializeComplexProperties(StandartDeliveryPeriod model)
    {

        Locality = LocalityWrapper.GetWrapper(model.Locality);

    }

  }
}
