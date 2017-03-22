using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    public LocalityWrapper(Locality model) : base(model) { }
    public LocalityWrapper(Locality model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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

		if (model.LocalityType != null)
		{
			if (ExistsWrappers.ContainsKey(model.LocalityType))
			{
				LocalityType = (LocalityTypeWrapper)ExistsWrappers[model.LocalityType];
			}
			else
			{
				LocalityType = new LocalityTypeWrapper(model.LocalityType, ExistsWrappers);
				RegisterComplexProperty(LocalityType);
			}
		}


		if (model.DistrictsRegion != null)
		{
			if (ExistsWrappers.ContainsKey(model.DistrictsRegion))
			{
				DistrictsRegion = (DistrictsRegionWrapper)ExistsWrappers[model.DistrictsRegion];
			}
			else
			{
				DistrictsRegion = new DistrictsRegionWrapper(model.DistrictsRegion, ExistsWrappers);
				RegisterComplexProperty(DistrictsRegion);
			}
		}


    }

  }
}
