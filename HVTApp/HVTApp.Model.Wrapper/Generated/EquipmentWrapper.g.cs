using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EquipmentWrapper : WrapperBase<Equipment>
  {
    public EquipmentWrapper(Equipment model) : base(model) { }
    public EquipmentWrapper(Equipment model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String DesignationType
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationTypeOriginalValue => GetOriginalValue<System.String>(nameof(DesignationType));
    public bool DesignationTypeIsChanged => GetIsChanged(nameof(DesignationType));

    public System.String DesignationSeries
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationSeriesOriginalValue => GetOriginalValue<System.String>(nameof(DesignationSeries));
    public bool DesignationSeriesIsChanged => GetIsChanged(nameof(DesignationSeries));

    public System.String DesignationOptions
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOptionsOriginalValue => GetOriginalValue<System.String>(nameof(DesignationOptions));
    public bool DesignationOptionsIsChanged => GetIsChanged(nameof(DesignationOptions));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<TechLinkWrapper> Links { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(Equipment model)
    {
      if (model.Links == null) throw new ArgumentException("Links cannot be null");
      Links = new ValidatableChangeTrackingCollection<TechLinkWrapper>(model.Links.Select(e => new TechLinkWrapper(e, ExistsWrappers)));
      RegisterCollection(Links, model.Links);

    }
  }
}
