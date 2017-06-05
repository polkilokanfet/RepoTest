using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
  {
    public ParameterGroupWrapper() : base(new ParameterGroup(), new Dictionary<IBaseEntity, object>()) { }
    public ParameterGroupWrapper(ParameterGroup model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public ParameterGroupWrapper(ParameterGroup model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Boolean IsOnlyChoice
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsOnlyChoiceOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsOnlyChoice));
    public bool IsOnlyChoiceIsChanged => GetIsChanged(nameof(IsOnlyChoice));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public MeasureWrapper Measure 
    {
        get { return GetComplexProperty<MeasureWrapper, Measure>(Model.Measure); }
        set { SetComplexProperty<MeasureWrapper, Measure>(Measure, value); }
    }

    public MeasureWrapper MeasureOriginalValue { get; private set; }
    public bool MeasureIsChanged => GetIsChanged(nameof(Measure));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(ParameterGroup model)
    {
        Measure = GetWrapper<MeasureWrapper, Measure>(model.Measure);
    }
  
    protected override void InitializeCollectionComplexProperties(ParameterGroup model)
    {
      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);

    }
  }
}
