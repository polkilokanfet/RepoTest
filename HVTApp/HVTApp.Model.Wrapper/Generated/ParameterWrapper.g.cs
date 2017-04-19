using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ParameterWrapper : WrapperBase<Parameter>
  {
    protected ParameterWrapper(Parameter model) : base(model) { }

	public static ParameterWrapper GetWrapper()
	{
		return GetWrapper(new Parameter());
	}

	public static ParameterWrapper GetWrapper(Parameter model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ParameterWrapper)Repository.ModelWrapperDictionary[model];

		return new ParameterWrapper(model);
	}



    #region SimpleProperties

    public System.String Value
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
    public bool ValueIsChanged => GetIsChanged(nameof(Value));


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


	public MeasureWrapper Measure 
    {
        get { return MeasureWrapper.GetWrapper(Model.Measure); }
        set
        {
			var oldPropVal = Measure;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public MeasureWrapper MeasureOriginalValue => MeasureWrapper.GetWrapper(GetOriginalValue<Measure>(nameof(Measure)));
    public bool MeasureIsChanged => GetIsChanged(nameof(Measure));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RequiredParentParametersWrapper> RequiredParentParametersList { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Parameter model)
    {

        Group = ParameterGroupWrapper.GetWrapper(model.Group);

        Measure = MeasureWrapper.GetWrapper(model.Measure);

    }

  
    protected override void InitializeCollectionComplexProperties(Parameter model)
    {

      if (model.RequiredParentParametersList == null) throw new ArgumentException("RequiredParentParametersList cannot be null");
      RequiredParentParametersList = new ValidatableChangeTrackingCollection<RequiredParentParametersWrapper>(model.RequiredParentParametersList.Select(e => RequiredParentParametersWrapper.GetWrapper(e)));
      RegisterCollection(RequiredParentParametersList, model.RequiredParentParametersList);


    }

  }
}
