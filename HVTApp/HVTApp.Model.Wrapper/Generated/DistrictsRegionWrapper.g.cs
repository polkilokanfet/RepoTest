using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictsRegionWrapper : WrapperBase<DistrictsRegion>
  {
    protected DistrictsRegionWrapper(DistrictsRegion model) : base(model) { }

	public static DistrictsRegionWrapper GetWrapper(DistrictsRegion model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (DistrictsRegionWrapper)Repository.ModelWrapperDictionary[model];

		return new DistrictsRegionWrapper(model);
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

	public DistrictWrapper District 
    {
        get { return DistrictWrapper.GetWrapper(Model.District); }
        set
        {
			var oldPropVal = District;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public DistrictWrapper DistrictOriginalValue => DistrictWrapper.GetWrapper(GetOriginalValue<District>(nameof(District)));
    public bool DistrictIsChanged => GetIsChanged(nameof(District));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(DistrictsRegion model)
    {

        District = DistrictWrapper.GetWrapper(model.District);

    }

  
    protected override void InitializeCollectionComplexProperties(DistrictsRegion model)
    {

      if (model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(model.Localities.Select(e => LocalityWrapper.GetWrapper(e)));
      RegisterCollection(Localities, model.Localities);


    }

  }
}
