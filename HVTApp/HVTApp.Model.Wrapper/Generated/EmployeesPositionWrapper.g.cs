using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeesPositionWrapper : WrapperBase<EmployeesPosition>
  {
    public EmployeesPositionWrapper() : base(new EmployeesPosition(), new Dictionary<IBaseEntity, object>()) { }
    public EmployeesPositionWrapper(EmployeesPosition model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public EmployeesPositionWrapper(EmployeesPosition model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public EmployeesPositionWrapper(EmployeesPosition model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
  }
}
