using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RequiredParametersWrapper : WrapperBase<RequiredParameters>
  {
    public RequiredParametersWrapper() : base(new RequiredParameters(), new Dictionary<IBaseEntity, object>()) { }
    public RequiredParametersWrapper(RequiredParameters model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public RequiredParametersWrapper(RequiredParameters model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
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

    #endregion
  
    protected override void InitializeCollectionComplexProperties(RequiredParameters model)
    {
      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);

    }
  }
}
