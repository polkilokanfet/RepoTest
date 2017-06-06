using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class RequiredProductsChildsWrapper : WrapperBase<RequiredProductsChilds>
  {
    public RequiredProductsChildsWrapper() : base(new RequiredProductsChilds(), new Dictionary<IBaseEntity, object>()) { }
    public RequiredProductsChildsWrapper(RequiredProductsChilds model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public RequiredProductsChildsWrapper(RequiredProductsChilds model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



    #region SimpleProperties

    public System.Int32 Count
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 CountOriginalValue => GetOriginalValue<System.Int32>(nameof(Count));
    public bool CountIsChanged => GetIsChanged(nameof(Count));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ParameterWrapper> MainProductParameters { get; private set; }


    public IValidatableChangeTrackingCollection<ParameterWrapper> ChildProductParameters { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(RequiredProductsChilds model)
    {

      if (model.MainProductParameters == null) throw new ArgumentException("MainProductParameters cannot be null");
      MainProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.MainProductParameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(MainProductParameters, model.MainProductParameters);


      if (model.ChildProductParameters == null) throw new ArgumentException("ChildProductParameters cannot be null");
      ChildProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.ChildProductParameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(ChildProductParameters, model.ChildProductParameters);


    }

  }
}
