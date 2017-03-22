using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    public LocalityWrapper(Locality model) : base(model) { }
    public LocalityWrapper(Locality model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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

	public LocalityTypeWrapper LocalityType
	{
		get { return GetComplexProperty<LocalityType, LocalityTypeWrapper>(nameof(LocalityType)); }
		set { SetComplexProperty<LocalityType, LocalityTypeWrapper>(value, nameof(LocalityType)); }
	}


	public DistrictsRegionWrapper DistrictsRegion
	{
		get { return GetComplexProperty<DistrictsRegion, DistrictsRegionWrapper>(nameof(DistrictsRegion)); }
		set { SetComplexProperty<DistrictsRegion, DistrictsRegionWrapper>(value, nameof(DistrictsRegion)); }
	}


    #endregion

    protected override void InitializeComplexProperties(Locality model)
    {

        LocalityType = GetWrapper<LocalityType, LocalityTypeWrapper>(model.LocalityType);

        DistrictsRegion = GetWrapper<DistrictsRegion, DistrictsRegionWrapper>(model.DistrictsRegion);

    }

  }
}
