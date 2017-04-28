using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class ParameterGroupWrapper : WrapperBase<ParameterGroup>
  {
    public ParameterGroupWrapper() : base(new ParameterGroup()) { }
    public ParameterGroupWrapper(ParameterGroup model) : base(model) { }
    public ParameterGroupWrapper(ParameterGroup model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public ParameterGroupWrapper(ParameterGroup model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> Parameters { get; private set; }


    public IValidatableChangeTrackingCollection<MeasureWrapper> Measures { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(ParameterGroup model)
    {

      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);


      if (model.Measures == null) throw new ArgumentException("Measures cannot be null");
      Measures = new ValidatableChangeTrackingCollection<MeasureWrapper>(model.Measures.Select(e => GetWrapper<MeasureWrapper, Measure>(e)));
      RegisterCollection(Measures, model.Measures);


    }

  }
}
