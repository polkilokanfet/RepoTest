using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RequiredPreviousParametersWrapper : WrapperBase<RequiredPreviousParameters>
  {
    public RequiredPreviousParametersWrapper(RequiredPreviousParameters model) : base(model) { }



    #region SimpleProperties

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ParameterWrapper Parameter { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> RequiredParameters { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Parameter = new ParameterWrapper(Model.Parameter);
		RegisterComplex(Parameter);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.RequiredParameters == null) throw new ArgumentException("RequiredParameters cannot be null");
      RequiredParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.RequiredParameters.Select(e => new ParameterWrapper(e)));
      RegisterCollection(RequiredParameters, Model.RequiredParameters);


    }

  }
}
