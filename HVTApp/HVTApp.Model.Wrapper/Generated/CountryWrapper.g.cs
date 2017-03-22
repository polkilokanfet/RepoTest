using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CountryWrapper : WrapperBase<Country>
  {
    public CountryWrapper(Country model) : base(model) { }
    public CountryWrapper(Country model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<DistrictWrapper> Districts { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(Country model)
    {
      if (model.Districts == null) throw new ArgumentException("Districts cannot be null");
      Districts = new ValidatableChangeTrackingCollection<DistrictWrapper>(model.Districts.Select(e => new DistrictWrapper(e, ExistsWrappers)));
      RegisterCollection(Districts, model.Districts);

    }
  }
}
