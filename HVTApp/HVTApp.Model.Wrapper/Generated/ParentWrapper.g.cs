using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ParentWrapper : WrapperBase<Parent>
  {
    protected ParentWrapper(Parent model) : base(model) { }
    //public ParentWrapper(Parent model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ParentWrapper GetWrapper(Parent model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ParentWrapper)Repository.ModelWrapperDictionary[model];

		return new ParentWrapper(model);
	}



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

	private ChildWrapper _fieldChild;
	public ChildWrapper Child 
    {
        get { return _fieldChild; }
        set
        {
            if (Equals(_fieldChild, value))
                return;

            UnRegisterComplexProperty(_fieldChild);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldChild = value;
        }
    }


    #endregion

    protected override void InitializeComplexProperties(Parent model)
    {

        Child = ChildWrapper.GetWrapper(model.Child);

    }

  }
}
