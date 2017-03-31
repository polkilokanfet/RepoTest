using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ChildWrapper : WrapperBase<Child>
  {
    public ChildWrapper(Child model) : base(model) { }
    public ChildWrapper(Child model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.Int32 N
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 NOriginalValue => GetOriginalValue<System.Int32>(nameof(N));
    public bool NIsChanged => GetIsChanged(nameof(N));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ParentWrapper Parent
	{
		get { return GetComplexProperty<Parent, ParentWrapper>(nameof(Parent)); }
		set { SetComplexProperty<Parent, ParentWrapper>(value, nameof(Parent)); }
	}


    #endregion

    protected override void InitializeComplexProperties(Child model)
    {

        Parent = GetWrapper<Parent, ParentWrapper>(model.Parent);

    }

  }
}
