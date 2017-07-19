using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ParameterWrapper : WrapperBase<Parameter>
  {
    private ParameterWrapper(IGetWrapper getWrapper) : base(new Parameter(), getWrapper) { }
    private ParameterWrapper(Parameter model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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

	public ParameterGroupWrapper Group 
    {
        get { return GetComplexProperty<ParameterGroupWrapper, ParameterGroup>(Model.Group); }
        set { SetComplexProperty<ParameterGroupWrapper, ParameterGroup>(Group, value); }
    }

    public ParameterGroupWrapper GroupOriginalValue { get; private set; }
    public bool GroupIsChanged => GetIsChanged(nameof(Group));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RequiredPreviousParametersWrapper> RequiredPreviousParameters { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Group = GetWrapper<ParameterGroupWrapper, ParameterGroup>(Model.Group);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.RequiredPreviousParameters == null) throw new ArgumentException("RequiredPreviousParameters cannot be null");
      RequiredPreviousParameters = new ValidatableChangeTrackingCollection<RequiredPreviousParametersWrapper>(Model.RequiredPreviousParameters.Select(e => GetWrapper<RequiredPreviousParametersWrapper, RequiredPreviousParameters>(e)));
      RegisterCollection(RequiredPreviousParameters, Model.RequiredPreviousParameters);


    }

  }
}
