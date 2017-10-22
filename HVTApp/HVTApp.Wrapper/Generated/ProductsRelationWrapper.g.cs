using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
  public partial class ProductsRelationWrapper : WrapperBase<ProductsRelation>
  {
    public ProductsRelationWrapper(ProductsRelation model) : base(model) { }



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

    public IValidatableChangeTrackingCollection<ParameterWrapper> ParentProductParameters { get; private set; }


    public IValidatableChangeTrackingCollection<ParameterWrapper> ChildProductParameters { get; private set; }


    #endregion

  
    protected override void InitializeCollectionProperties()
    {

      if (Model.ParentProductParameters == null) throw new ArgumentException("ParentProductParameters cannot be null");
      ParentProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ParentProductParameters.Select(e => new ParameterWrapper(e)));
      RegisterCollection(ParentProductParameters, Model.ParentProductParameters);


      if (Model.ChildProductParameters == null) throw new ArgumentException("ChildProductParameters cannot be null");
      ChildProductParameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(Model.ChildProductParameters.Select(e => new ParameterWrapper(e)));
      RegisterCollection(ChildProductParameters, Model.ChildProductParameters);


    }

  }
}
