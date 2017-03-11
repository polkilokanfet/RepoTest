using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ActivityFildWrapper : WrapperBase<ActivityFild>
  {
    public ActivityFildWrapper(ActivityFild model) : base(model) { }
    public ActivityFildWrapper(ActivityFild model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public HVTApp.Model.FieldOfActivity FieldOfActivity
    {
      get { return GetValue<HVTApp.Model.FieldOfActivity>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.FieldOfActivity FieldOfActivityOriginalValue => GetOriginalValue<HVTApp.Model.FieldOfActivity>(nameof(FieldOfActivity));
    public bool FieldOfActivityIsChanged => GetIsChanged(nameof(FieldOfActivity));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public CompanyWrapper Company { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(ActivityFild model)
    {
      if (model.Company == null) throw new ArgumentException("Company cannot be null");
      if (ExistsWrappers.ContainsKey(model.Company))
      {
          Company = (CompanyWrapper)ExistsWrappers[model.Company];
      }
      else
      {
          Company = new CompanyWrapper(model.Company, ExistsWrappers);
          RegisterComplexProperty(Company);
      }

    }
  }
}
