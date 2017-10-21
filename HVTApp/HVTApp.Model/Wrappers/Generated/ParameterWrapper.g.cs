using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ParameterWrapper : WrapperBase<Parameter>
  {
    public ParameterWrapper(Parameter model) : base(model) { }



    #region SimpleProperties

    public System.String Value
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
    public bool ValueIsChanged => GetIsChanged(nameof(Value));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private ParameterGroupWrapper _fieldGroup;
	public ParameterGroupWrapper Group 
    {
        get { return _fieldGroup ; }
        set
        {
            SetComplexValue<ParameterGroup, ParameterGroupWrapper>(_fieldGroup, value);
            _fieldGroup  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RequiredPreviousParametersWrapper> RequiredPreviousParameters { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Group != null)
        {
            _fieldGroup = new ParameterGroupWrapper(Model.Group);
            RegisterComplex(Group);
        }

    }

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.RequiredPreviousParameters == null) throw new ArgumentException("RequiredPreviousParameters cannot be null");
      RequiredPreviousParameters = new ValidatableChangeTrackingCollection<RequiredPreviousParametersWrapper>(Model.RequiredPreviousParameters.Select(e => new RequiredPreviousParametersWrapper(e)));
      RegisterCollection(RequiredPreviousParameters, Model.RequiredPreviousParameters);


    }

  }
}
