using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    protected LocalityWrapper(Locality model) : base(model) { }

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
            UnRegisterComplexProperty(LocalityType);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	public DistrictsRegionWrapper DistrictsRegion 
    {
        get { return DistrictsRegionWrapper.GetWrapper(Model.DistrictsRegion); }
        set
        {
            UnRegisterComplexProperty(DistrictsRegion);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(Locality model)
    {

        LocalityType = LocalityTypeWrapper.GetWrapper(model.LocalityType);

        DistrictsRegion = DistrictsRegionWrapper.GetWrapper(model.DistrictsRegion);

    }

  }
}
