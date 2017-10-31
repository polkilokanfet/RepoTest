using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class DistrictWrapper : WrapperBase<District>
	{
	public DistrictWrapper(District model) : base(model) { }

	
    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Guid CountryId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid CountryIdOriginalValue => GetOriginalValue<System.Guid>(nameof(CountryId));
    public bool CountryIdIsChanged => GetIsChanged(nameof(CountryId));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<RegionWrapper> Regions { get; private set; }

    #endregion
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.Regions == null) throw new ArgumentException("Regions cannot be null");
      Regions = new ValidatableChangeTrackingCollection<RegionWrapper>(Model.Regions.Select(e => new RegionWrapper(e)));
      RegisterCollection(Regions, Model.Regions);

    }
	}
}
	