using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechParameterWrapper : WrapperBase<TechParameter>
  {
    protected TechParameterWrapper(TechParameter model) : base(model) { }
    //public TechParameterWrapper(TechParameter model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static TechParameterWrapper GetWrapper(TechParameter model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TechParameterWrapper)Repository.ModelWrapperDictionary[model];

		return new TechParameterWrapper(model);
	}



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

	private TechParametersGroupWrapper _fieldGroup;
	public TechParametersGroupWrapper Group 
    {
        get { return _fieldGroup; }
        set
        {
            if (Equals(_fieldGroup, value))
                return;

            UnRegisterComplexProperty(_fieldGroup);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldGroup = value;
        }
    }


    #endregion

    protected override void InitializeComplexProperties(TechParameter model)
    {

        Group = TechParametersGroupWrapper.GetWrapper(model.Group);

    }

  }
}
