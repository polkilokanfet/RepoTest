using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechParametersGroupWrapper : WrapperBase<TechParametersGroup>
  {
    public TechParametersGroupWrapper(TechParametersGroup model) : base(model) { }
    public TechParametersGroupWrapper(TechParametersGroup model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Boolean IsOntyChoice
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsOntyChoiceOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsOntyChoice));
    public bool IsOntyChoiceIsChanged => GetIsChanged(nameof(IsOntyChoice));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<TechParameterWrapper> TechParameters { get; private set; }

    #endregion
  
    protected override void InitializeCollectionComplexProperties(TechParametersGroup model)
    {
      if (model.TechParameters == null) throw new ArgumentException("TechParameters cannot be null");
      TechParameters = new ValidatableChangeTrackingCollection<TechParameterWrapper>(model.TechParameters.Select(e => new TechParameterWrapper(e, ExistsWrappers)));
      RegisterCollection(TechParameters, model.TechParameters);

    }
  }
}
