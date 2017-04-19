using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class MeasureWrapper : WrapperBase<Measure>
  {
    protected MeasureWrapper(Measure model) : base(model) { }

	public static MeasureWrapper GetWrapper()
	{
		return GetWrapper(new Measure());
	}

	public static MeasureWrapper GetWrapper(Measure model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (MeasureWrapper)Repository.ModelWrapperDictionary[model];

		return new MeasureWrapper(model);
	}



    #region SimpleProperties

    public System.String FullName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
    public bool FullNameIsChanged => GetIsChanged(nameof(FullName));


    public System.String ShortName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
    public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ParameterGroupWrapper Group 
    {
        get { return ParameterGroupWrapper.GetWrapper(Model.Group); }
        set
        {
			var oldPropVal = Group;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ParameterGroupWrapper GroupOriginalValue => ParameterGroupWrapper.GetWrapper(GetOriginalValue<ParameterGroup>(nameof(Group)));
    public bool GroupIsChanged => GetIsChanged(nameof(Group));


    #endregion

    protected override void InitializeComplexProperties(Measure model)
    {

        Group = ParameterGroupWrapper.GetWrapper(model.Group);

    }

  }
}
