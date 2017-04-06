using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechParameterWrapper : WrapperBase<TechParameter>
  {
    protected TechParameterWrapper(TechParameter model) : base(model) { }

	public static TechParameterWrapper GetWrapper()
	{
		return GetWrapper(new TechParameter());
	}

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

	public TechParametersGroupWrapper Group 
    {
        get { return TechParametersGroupWrapper.GetWrapper(Model.Group); }
        set
        {
			var oldPropVal = Group;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TechParametersGroupWrapper GroupOriginalValue => TechParametersGroupWrapper.GetWrapper(GetOriginalValue<TechParametersGroup>(nameof(Group)));
    public bool GroupIsChanged => GetIsChanged(nameof(Group));


    #endregion

    protected override void InitializeComplexProperties(TechParameter model)
    {

        Group = TechParametersGroupWrapper.GetWrapper(model.Group);

    }

  }
}
