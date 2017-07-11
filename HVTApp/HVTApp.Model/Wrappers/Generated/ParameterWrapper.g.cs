using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ParameterWrapper : WrapperBase<Parameter>
  {
    private ParameterWrapper() : base(new Parameter()) { }
    private ParameterWrapper(Parameter model) : base(model) { }



    #region SimpleProperties

    public System.String Value
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ValueOriginalValue => GetOriginalValue<System.String>(nameof(Value));
    public bool ValueIsChanged => GetIsChanged(nameof(Value));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
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

    public IValidatableChangeTrackingCollection<RequiredParametersWrapper> RequiredParents { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Group = GetWrapper<ParameterGroupWrapper, ParameterGroup>(Model.Group);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.RequiredPreviousParameters == null) throw new ArgumentException("RequiredPreviousParameters cannot be null");
      RequiredParents = new ValidatableChangeTrackingCollection<RequiredParametersWrapper>(Model.RequiredPreviousParameters.Select(e => GetWrapper<RequiredParametersWrapper, RequiredPreviousParameters>(e)));
      RegisterCollection(RequiredParents, Model.RequiredPreviousParameters);


    }

  }
}
