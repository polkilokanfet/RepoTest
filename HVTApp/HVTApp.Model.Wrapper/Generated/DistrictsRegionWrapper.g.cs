using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictsRegionWrapper : WrapperBase<DistrictsRegion>
  {
    public DistrictsRegionWrapper(DistrictsRegion model) : base(model) { }
    public DistrictsRegionWrapper(DistrictsRegion model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public DistrictWrapper District { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<LocalityWrapper> Localities { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(DistrictsRegion model)
    {
      if (model.District == null) throw new ArgumentException("District cannot be null");
      if (ExistsWrappers.ContainsKey(model.District))
      {
          District = (DistrictWrapper)ExistsWrappers[model.District];
      }
      else
      {
          District = new DistrictWrapper(model.District, ExistsWrappers);
          RegisterComplexProperty(District);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(DistrictsRegion model)
    {
      if (model.Localities == null) throw new ArgumentException("Localities cannot be null");
      Localities = new ValidatableChangeTrackingCollection<LocalityWrapper>(model.Localities.Select(e => new LocalityWrapper(e, ExistsWrappers)));
      RegisterCollection(Localities, model.Localities);

    }
  }
}
