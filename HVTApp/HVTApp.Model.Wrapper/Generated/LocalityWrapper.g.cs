using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    protected LocalityWrapper(Locality model) : base(model) { }

	public static LocalityWrapper GetWrapper()
	{
		return GetWrapper(new Locality());
	}

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
        get { return LocalityTypeWrapper.GetWrapper(Model.LocalityType); }
        set
        {
			var oldPropVal = LocalityType;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public LocalityTypeWrapper LocalityTypeOriginalValue => LocalityTypeWrapper.GetWrapper(GetOriginalValue<LocalityType>(nameof(LocalityType)));
    public bool LocalityTypeIsChanged => GetIsChanged(nameof(LocalityType));


	public DistrictsRegionWrapper DistrictsRegion 
    {
        get { return DistrictsRegionWrapper.GetWrapper(Model.DistrictsRegion); }
        set
        {
			var oldPropVal = DistrictsRegion;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public DistrictsRegionWrapper DistrictsRegionOriginalValue => DistrictsRegionWrapper.GetWrapper(GetOriginalValue<DistrictsRegion>(nameof(DistrictsRegion)));
    public bool DistrictsRegionIsChanged => GetIsChanged(nameof(DistrictsRegion));


    #endregion

    protected override void InitializeComplexProperties(Locality model)
    {

        LocalityType = LocalityTypeWrapper.GetWrapper(model.LocalityType);

        DistrictsRegion = DistrictsRegionWrapper.GetWrapper(model.DistrictsRegion);

    }

  }
}
