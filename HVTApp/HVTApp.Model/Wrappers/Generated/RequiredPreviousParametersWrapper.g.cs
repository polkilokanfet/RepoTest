using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RequiredPreviousParametersWrapper : WrapperBase<RequiredPreviousParameters>
  {
    private RequiredPreviousParametersWrapper() : base(new RequiredPreviousParameters()) { }
    private RequiredPreviousParametersWrapper(RequiredPreviousParameters model) : base(model) { }



    #region SimpleProperties

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ParameterWrapper Parameter 
    {
        get { return GetComplexProperty<ParameterWrapper, Parameter>(Model.Parameter); }
        set { SetComplexProperty<ParameterWrapper, Parameter>(Parameter, value); }
    }

    public ParameterWrapper ParameterOriginalValue { get; private set; }
    public bool ParameterIsChanged => GetIsChanged(nameof(Parameter));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> RequiredParameters { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Parameter = GetWrapper<ParameterWrapper, Parameter>(Model.Parameter);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.RequiredParameters == null) throw new ArgumentException("RequiredParameters cannot be null");
      RequiredParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.RequiredParameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(RequiredParameters, Model.RequiredParameters);


    }

  }
}
