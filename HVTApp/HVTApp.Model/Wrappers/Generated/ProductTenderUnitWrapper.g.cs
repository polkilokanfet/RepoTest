using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class ProductTenderUnitWrapper : WrapperBase<ProductTenderUnit>
  {
    private ProductTenderUnitWrapper() : base(new ProductTenderUnit()) { }
    private ProductTenderUnitWrapper(ProductTenderUnit model) : base(model) { }



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

	public ProductComplexUnitWrapper ProductComplexUnit 
    {
        get { return GetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit); }
        set { SetComplexProperty<ProductComplexUnitWrapper, ProductComplexUnit>(ProductComplexUnit, value); }
    }

    public ProductComplexUnitWrapper ProductComplexUnitOriginalValue { get; private set; }
    public bool ProductComplexUnitIsChanged => GetIsChanged(nameof(ProductComplexUnit));


	public TenderWrapper Tender 
    {
        get { return GetComplexProperty<TenderWrapper, Tender>(Model.Tender); }
        set { SetComplexProperty<TenderWrapper, Tender>(Tender, value); }
    }

    public TenderWrapper TenderOriginalValue { get; private set; }
    public bool TenderIsChanged => GetIsChanged(nameof(Tender));


	public ProductWrapper Product 
    {
        get { return GetComplexProperty<ProductWrapper, Product>(Model.Product); }
        set { SetComplexProperty<ProductWrapper, Product>(Product, value); }
    }

    public ProductWrapper ProductOriginalValue { get; private set; }
    public bool ProductIsChanged => GetIsChanged(nameof(Product));


	public CostWrapper Cost 
    {
        get { return GetComplexProperty<CostWrapper, Cost>(Model.Cost); }
        set { SetComplexProperty<CostWrapper, Cost>(Cost, value); }
    }

    public CostWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


	public CompanyWrapper ProducerWinner 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.ProducerWinner); }
        set { SetComplexProperty<CompanyWrapper, Company>(ProducerWinner, value); }
    }

    public CompanyWrapper ProducerWinnerOriginalValue { get; private set; }
    public bool ProducerWinnerIsChanged => GetIsChanged(nameof(ProducerWinner));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<PaymentConditionWrapper> PaymentsConditions { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        ProductComplexUnit = GetWrapper<ProductComplexUnitWrapper, ProductComplexUnit>(Model.ProductComplexUnit);

        Tender = GetWrapper<TenderWrapper, Tender>(Model.Tender);

        Product = GetWrapper<ProductWrapper, Product>(Model.Product);

        Cost = GetWrapper<CostWrapper, Cost>(Model.Cost);

        ProducerWinner = GetWrapper<CompanyWrapper, Company>(Model.ProducerWinner);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(Model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, Model.PaymentsConditions);


    }

  }
}
