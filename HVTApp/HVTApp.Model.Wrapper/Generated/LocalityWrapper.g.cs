using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    protected LocalityWrapper(Locality model) : base(model) { }
    //public LocalityWrapper(Locality model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private LocalityTypeWrapper _fieldLocalityType;
	public LocalityTypeWrapper LocalityType 
    {
        get { return _fieldLocalityType; }
        set
        {
            if (Equals(_fieldLocalityType, value))
                return;

            UnRegisterComplexProperty(_fieldLocalityType);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldLocalityType = value;
        }
    }


	private DistrictsRegionWrapper _fieldDistrictsRegion;
	public DistrictsRegionWrapper DistrictsRegion 
    {
        get { return _fieldDistrictsRegion; }
        set
        {
            if (Equals(_fieldDistrictsRegion, value))
                return;

            UnRegisterComplexProperty(_fieldDistrictsRegion);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldDistrictsRegion = value;
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
