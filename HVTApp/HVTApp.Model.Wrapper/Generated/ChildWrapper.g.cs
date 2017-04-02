using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ChildWrapper : WrapperBase<Child>
  {
    protected ChildWrapper(Child model) : base(model) { }
    //public ChildWrapper(Child model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ChildWrapper GetWrapper(Child model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ChildWrapper)Repository.ModelWrapperDictionary[model];

		return new ChildWrapper(model);
	}



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

	private ParentWrapper _fieldParent;
	public ParentWrapper Parent 
    {
        get { return _fieldParent; }
        set
        {
            if (Equals(_fieldParent, value))
                return;

            UnRegisterComplexProperty(_fieldParent);

            _fieldParent = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(Child model)
    {

        Parent = ParentWrapper.GetWrapper(model.Parent);

    }

  }
}
