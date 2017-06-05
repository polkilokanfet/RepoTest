using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class TendersUnitWrapper : WrapperBase<TendersUnit>
  {
    public TendersUnitWrapper() : base(new TendersUnit(), new Dictionary<IBaseEntity, object>()) { }
    public TendersUnitWrapper(TendersUnit model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public TendersUnitWrapper(TendersUnit model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
	public UnitWrapper Unit 
    {
        get { return GetComplexProperty<UnitWrapper, Unit>(Model.Unit); }
        set { SetComplexProperty<UnitWrapper, Unit>(Unit, value); }
    }

    public UnitWrapper UnitOriginalValue { get; private set; }
    public bool UnitIsChanged => GetIsChanged(nameof(Unit));

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

	public SumAndVatWrapper Cost 
    {
        get { return GetComplexProperty<SumAndVatWrapper, SumAndVat>(Model.Cost); }
        set { SetComplexProperty<SumAndVatWrapper, SumAndVat>(Cost, value); }
    }

    public SumAndVatWrapper CostOriginalValue { get; private set; }
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
    protected override void InitializeComplexProperties(TendersUnit model)
    {
        Unit = GetWrapper<UnitWrapper, Unit>(model.Unit);
        Tender = GetWrapper<TenderWrapper, Tender>(model.Tender);
        Product = GetWrapper<ProductWrapper, Product>(model.Product);
        Cost = GetWrapper<SumAndVatWrapper, SumAndVat>(model.Cost);
        ProducerWinner = GetWrapper<CompanyWrapper, Company>(model.ProducerWinner);
    }
  
    protected override void InitializeCollectionComplexProperties(TendersUnit model)
    {
      if (model.PaymentsConditions == null) throw new ArgumentException("PaymentsConditions cannot be null");
      PaymentsConditions = new ValidatableChangeTrackingCollection<PaymentConditionWrapper>(model.PaymentsConditions.Select(e => GetWrapper<PaymentConditionWrapper, PaymentCondition>(e)));
      RegisterCollection(PaymentsConditions, model.PaymentsConditions);

    }
  }
}
