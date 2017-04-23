using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class MeasureWrapper : WrapperBase<Measure>
  {
    public MeasureWrapper() : base(new Measure()) { }
    public MeasureWrapper(Measure model) : base(model) { }

//	public static MeasureWrapper GetWrapper()
//	{
//		return GetWrapper(new Measure());
//	}
//
//	public static MeasureWrapper GetWrapper(Measure model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (MeasureWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new MeasureWrapper(model);
//	}


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
	private ParameterGroupWrapper _fieldGroup;
	public ParameterGroupWrapper Group 
    {
        get { return _fieldGroup; }
        set
        {
			SetComplexProperty<ParameterGroupWrapper, ParameterGroup>(_fieldGroup, value);
			_fieldGroup = value;
        }
    }
    public ParameterGroupWrapper GroupOriginalValue { get; private set; }
    public bool GroupIsChanged => GetIsChanged(nameof(Group));

    #endregion
    protected override void InitializeComplexProperties(Measure model)
    {
        Group = GetWrapper<ParameterGroupWrapper, ParameterGroup>(model.Group);
    }
  }
}
