using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class RequiredParentParametersWrapper : WrapperBase<RequiredParentParameters>
  {
    public RequiredParentParametersWrapper() : base(new RequiredParentParameters()) { }
    public RequiredParentParametersWrapper(RequiredParentParameters model) : base(model) { }
    public RequiredParentParametersWrapper(RequiredParentParameters model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }



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

  
    protected override void InitializeCollectionComplexProperties(RequiredParentParameters model)
    {

      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);


    }

  }
}
