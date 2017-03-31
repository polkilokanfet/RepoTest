using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ParentWrapper : WrapperBase<Parent>
  {
    public ParentWrapper(Parent model) : base(model) { }
    public ParentWrapper(Parent model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ChildWrapper Child
	{
		get { return GetComplexProperty<Child, ChildWrapper>(nameof(Child)); }
		set { SetComplexProperty<Child, ChildWrapper>(value, nameof(Child)); }
	}


    #endregion

    protected override void InitializeComplexProperties(Parent model)
    {

        Child = GetWrapper<Child, ChildWrapper>(model.Child);

    }

  }
}
