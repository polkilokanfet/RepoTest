using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
  public partial class RequiredPreviousParametersWrapper : WrapperBase<RequiredPreviousParameters>
  {
    public RequiredPreviousParametersWrapper(RequiredPreviousParameters model) : base(model) { }



    #region SimpleProperties

    public System.Guid ParameterId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid ParameterIdOriginalValue => GetOriginalValue<System.Guid>(nameof(ParameterId));
    public bool ParameterIdIsChanged => GetIsChanged(nameof(ParameterId));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> RequiredParameters { get; private set; }


    #endregion

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.RequiredParameters == null) throw new ArgumentException("RequiredParameters cannot be null");
      RequiredParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.RequiredParameters.Select(e => new ParameterWrapper(e)));
      RegisterCollection(RequiredParameters, Model.RequiredParameters);


    }

  }
}
