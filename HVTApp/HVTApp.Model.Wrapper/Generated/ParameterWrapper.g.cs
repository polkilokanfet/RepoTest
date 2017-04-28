using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class ParameterWrapper : WrapperBase<Parameter>
  {
    public ParameterWrapper() : base(new Parameter()) { }
    public ParameterWrapper(Parameter model) : base(model) { }
    public ParameterWrapper(Parameter model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public ParameterWrapper(Parameter model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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
        get { return GetComplexProperty<ParameterGroupWrapper, ParameterGroup>(Model.Group); }
        set { SetComplexProperty<ParameterGroupWrapper, ParameterGroup>(Group, value); }
    }

    public ParameterGroupWrapper GroupOriginalValue { get; private set; }
    public bool GroupIsChanged => GetIsChanged(nameof(Group));


	public MeasureWrapper Measure 
    {
        get { return GetComplexProperty<MeasureWrapper, Measure>(Model.Measure); }
        set { SetComplexProperty<MeasureWrapper, Measure>(Measure, value); }
    }

    public MeasureWrapper MeasureOriginalValue { get; private set; }
    public bool MeasureIsChanged => GetIsChanged(nameof(Measure));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RequiredParentParametersWrapper> RequiredParentParametersList { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Parameter model)
    {

        Group = GetWrapper<ParameterGroupWrapper, ParameterGroup>(model.Group);

        Measure = GetWrapper<MeasureWrapper, Measure>(model.Measure);

    }

  
    protected override void InitializeCollectionComplexProperties(Parameter model)
    {

      if (model.RequiredParentParametersList == null) throw new ArgumentException("RequiredParentParametersList cannot be null");
      RequiredParentParametersList = new ValidatableChangeTrackingCollection<RequiredParentParametersWrapper>(model.RequiredParentParametersList.Select(e => GetWrapper<RequiredParentParametersWrapper, RequiredParentParameters>(e)));
      RegisterCollection(RequiredParentParametersList, model.RequiredParentParametersList);


    }

  }
}
