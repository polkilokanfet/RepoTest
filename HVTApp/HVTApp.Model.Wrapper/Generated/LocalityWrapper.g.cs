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

	public static LocalityWrapper GetWrapper(Locality model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (LocalityWrapper)Repository.ModelWrapperDictionary[model];

		return new LocalityWrapper(model);
	}



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

        LocalityType = LocalityTypeWrapper.GetWrapper(model.LocalityType);

        DistrictsRegion = DistrictsRegionWrapper.GetWrapper(model.DistrictsRegion);

    }

  }
}
