using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class RegionWrapper : WrapperBase<Region>
  {
    protected RegionWrapper(Region model) : base(model) { }

	public static RegionWrapper GetWrapper()
	{
		return GetWrapper(new Region());
	}

	public static RegionWrapper GetWrapper(Region model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (RegionWrapper)Repository.ModelWrapperDictionary[model];

		return new RegionWrapper(model);
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


	public LocalityWrapper Capital 
    {
        get { return LocalityWrapper.GetWrapper(Model.Capital); }
        set
        {
			var oldPropVal = Capital;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public LocalityWrapper CapitalOriginalValue => LocalityWrapper.GetWrapper(GetOriginalValue<Locality>(nameof(Capital)));
    public bool CapitalIsChanged => GetIsChanged(nameof(Capital));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Region model)
    {

        District = DistrictWrapper.GetWrapper(model.District);

        Capital = LocalityWrapper.GetWrapper(model.Capital);

    }

  
    protected override void InitializeCollectionComplexProperties(Region model)
    {

      if (model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(model.Localities.Select(e => LocalityWrapper.GetWrapper(e)));
      RegisterCollection(Localities, model.Localities);


    }

  }
}
