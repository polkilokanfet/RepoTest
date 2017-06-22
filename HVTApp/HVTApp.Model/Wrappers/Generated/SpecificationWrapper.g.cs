using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class SpecificationWrapper : WrapperBase<Specification>
  {
    private SpecificationWrapper() : base(new Specification()) { }
    private SpecificationWrapper(Specification model) : base(model) { }



    #region SimpleProperties

    public System.String Number
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NumberOriginalValue => GetOriginalValue<System.String>(nameof(Number));
    public bool NumberIsChanged => GetIsChanged(nameof(Number));


    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public ContractWrapper Contract 
    {
        get { return GetComplexProperty<ContractWrapper, Contract>(Model.Contract); }
        set { SetComplexProperty<ContractWrapper, Contract>(Contract, value); }
    }

    public ContractWrapper ContractOriginalValue { get; private set; }
    public bool ContractIsChanged => GetIsChanged(nameof(Contract));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductComplexUnitWrapper> ProductComplexUnits { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Contract = GetWrapper<ContractWrapper, Contract>(Model.Contract);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.ProductComplexUnits == null) throw new ArgumentException("ProductComplexUnits cannot be null");
      ProductComplexUnits = new ValidatableChangeTrackingCollection<ProductComplexUnitWrapper>(Model.ProductComplexUnits.Select(e => GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(e)));
      RegisterCollection(ProductComplexUnits, Model.ProductComplexUnits);


    }

  }
}
