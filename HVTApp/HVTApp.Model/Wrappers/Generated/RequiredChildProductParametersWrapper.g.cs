using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RequiredChildProductParametersWrapper : WrapperBase<RequiredChildProductParameters>
  {
    private RequiredChildProductParametersWrapper() : base(new RequiredChildProductParameters()) { }
    private RequiredChildProductParametersWrapper(RequiredChildProductParameters model) : base(model) { }



    #region SimpleProperties

    public System.Int32 Count
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));
    public bool CountIsChanged => GetIsChanged(nameof(Count));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> MainProductParameters { get; private set; }


    public IValidatableChangeTrackingCollection<ParameterWrapper> ChildProductParameters { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.MainProductParameters == null) throw new ArgumentException("MainProductParameters cannot be null");
      MainProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.MainProductParameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(MainProductParameters, Model.MainProductParameters);


      if (Model.ChildProductParameters == null) throw new ArgumentException("ChildProductParameters cannot be null");
      ChildProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ChildProductParameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(ChildProductParameters, Model.ChildProductParameters);


    }

  }
}
