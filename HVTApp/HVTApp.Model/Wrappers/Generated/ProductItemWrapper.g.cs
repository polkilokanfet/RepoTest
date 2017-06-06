using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductItemWrapper : WrapperBase<ProductItem>
  {
    public ProductItemWrapper() : base(new ProductItem(), new Dictionary<IBaseEntity, object>()) { }
    public ProductItemWrapper(ProductItem model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public ProductItemWrapper(ProductItem model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



    #region SimpleProperties

    public System.String Designation
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DesignationOriginalValue => GetOriginalValue<System.String>(nameof(Designation));
    public bool DesignationIsChanged => GetIsChanged(nameof(Designation));


    public System.String StructureCostNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String StructureCostNumberOriginalValue => GetOriginalValue<System.String>(nameof(StructureCostNumber));
    public bool StructureCostNumberIsChanged => GetIsChanged(nameof(StructureCostNumber));


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


    public IValidatableChangeTrackingCollection<SumOnDateWrapper> Prices { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(ProductItem model)
    {

      if (model.Parameters == null) throw new ArgumentException("Parameters cannot be null");
      Parameters = new ValidatableChangeTrackingCollection<ParameterWrapper>(model.Parameters.Select(e => GetWrapper<ParameterWrapper, Parameter>(e)));
      RegisterCollection(Parameters, model.Parameters);


      if (model.Prices == null) throw new ArgumentException("Prices cannot be null");
      Prices = new ValidatableChangeTrackingCollection<SumOnDateWrapper>(model.Prices.Select(e => GetWrapper<SumOnDateWrapper, SumOnDate>(e)));
      RegisterCollection(Prices, model.Prices);


    }

  }
}
