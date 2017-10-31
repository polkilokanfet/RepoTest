using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class LocalityWrapper : WrapperBase<Locality>
	{
	public LocalityWrapper(Locality model) : base(model) { }

	
    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Guid RegionId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid RegionIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RegionId));
    public bool RegionIdIsChanged => GetIsChanged(nameof(RegionId));

    public System.Boolean IsRegionCapital
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsRegionCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsRegionCapital));
    public bool IsRegionCapitalIsChanged => GetIsChanged(nameof(IsRegionCapital));

    public System.Boolean IsDistrictsCapital
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsDistrictsCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsDistrictsCapital));
    public bool IsDistrictsCapitalIsChanged => GetIsChanged(nameof(IsDistrictsCapital));

    public System.Boolean IsCountryCapital
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsCountryCapitalOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsCountryCapital));
    public bool IsCountryCapitalIsChanged => GetIsChanged(nameof(IsCountryCapital));

    public System.Nullable<System.Double> StandartDeliveryPeriod
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Double> StandartDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(StandartDeliveryPeriod));
    public bool StandartDeliveryPeriodIsChanged => GetIsChanged(nameof(StandartDeliveryPeriod));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private LocalityTypeWrapper _fieldLocalityType;
	public LocalityTypeWrapper LocalityType 
    {
        get { return _fieldLocalityType ; }
        set
        {
            SetComplexValue<LocalityType, LocalityTypeWrapper>(_fieldLocalityType, value);
            _fieldLocalityType  = value;
        }
    }
    #endregion
    public override void InitializeComplexProperties()
    {
        UnRegisterComplex(_fieldLocalityType);
        _fieldLocalityType = null;
		if (Model.LocalityType != null)
        {
            _fieldLocalityType = new LocalityTypeWrapper(Model.LocalityType);
            RegisterComplex(LocalityType);
        }

    }
	}
}
	