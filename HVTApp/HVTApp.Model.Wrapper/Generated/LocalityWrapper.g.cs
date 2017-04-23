using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    public LocalityWrapper() : base(new Locality()) { }
    public LocalityWrapper(Locality model) : base(model) { }

//	public static LocalityWrapper GetWrapper()
//	{
//		return GetWrapper(new Locality());
//	}
//
//	public static LocalityWrapper GetWrapper(Locality model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (LocalityWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new LocalityWrapper(model);
//	}


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private LocalityTypeWrapper _fieldLocalityType;
	public LocalityTypeWrapper LocalityType 
    {
        get { return _fieldLocalityType; }
        set
        {
			SetComplexProperty<LocalityTypeWrapper, LocalityType>(_fieldLocalityType, value);
			_fieldLocalityType = value;
        }
    }
    public LocalityTypeWrapper LocalityTypeOriginalValue { get; private set; }
    public bool LocalityTypeIsChanged => GetIsChanged(nameof(LocalityType));

	private RegionWrapper _fieldRegion;
	public RegionWrapper Region 
    {
        get { return _fieldRegion; }
        set
        {
			SetComplexProperty<RegionWrapper, Region>(_fieldRegion, value);
			_fieldRegion = value;
        }
    }
    public RegionWrapper RegionOriginalValue { get; private set; }
    public bool RegionIsChanged => GetIsChanged(nameof(Region));

	private StandartDeliveryPeriodWrapper _fieldDeliveryPeriod;
	public StandartDeliveryPeriodWrapper DeliveryPeriod 
    {
        get { return _fieldDeliveryPeriod; }
        set
        {
			SetComplexProperty<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(_fieldDeliveryPeriod, value);
			_fieldDeliveryPeriod = value;
        }
    }
    public StandartDeliveryPeriodWrapper DeliveryPeriodOriginalValue { get; private set; }
    public bool DeliveryPeriodIsChanged => GetIsChanged(nameof(DeliveryPeriod));

    #endregion
    protected override void InitializeComplexProperties(Locality model)
    {
        LocalityType = GetWrapper<LocalityTypeWrapper, LocalityType>(model.LocalityType);
        Region = GetWrapper<RegionWrapper, Region>(model.Region);
        DeliveryPeriod = GetWrapper<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(model.DeliveryPeriod);
    }
  }
}
