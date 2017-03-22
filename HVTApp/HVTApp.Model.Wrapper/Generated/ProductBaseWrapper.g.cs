using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductBaseWrapper : WrapperBase<ProductBase>
  {
    public ProductBaseWrapper(ProductBase model) : base(model) { }
    public ProductBaseWrapper(ProductBase model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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

	public ProductsMainGroupWrapper ProductsMainGroup
	{
		get { return GetComplexProperty<ProductsMainGroup, ProductsMainGroupWrapper>(nameof(ProductsMainGroup)); }
		set { SetComplexProperty<ProductsMainGroup, ProductsMainGroupWrapper>(value, nameof(ProductsMainGroup)); }
	}


	public EquipmentWrapper Equipment
	{
		get { return GetComplexProperty<Equipment, EquipmentWrapper>(nameof(Equipment)); }
		set { SetComplexProperty<Equipment, EquipmentWrapper>(value, nameof(Equipment)); }
	}


	public TenderInfoWrapper TenderInfo
	{
		get { return GetComplexProperty<TenderInfo, TenderInfoWrapper>(nameof(TenderInfo)); }
		set { SetComplexProperty<TenderInfo, TenderInfoWrapper>(value, nameof(TenderInfo)); }
	}


	public OrderInfoWrapper OrderInfo
	{
		get { return GetComplexProperty<OrderInfo, OrderInfoWrapper>(nameof(OrderInfo)); }
		set { SetComplexProperty<OrderInfo, OrderInfoWrapper>(value, nameof(OrderInfo)); }
	}


	public DateInfoWrapper DateInfo
	{
		get { return GetComplexProperty<DateInfo, DateInfoWrapper>(nameof(DateInfo)); }
		set { SetComplexProperty<DateInfo, DateInfoWrapper>(value, nameof(DateInfo)); }
	}


	public TermsInfoWrapper TermsInfo
	{
		get { return GetComplexProperty<TermsInfo, TermsInfoWrapper>(nameof(TermsInfo)); }
		set { SetComplexProperty<TermsInfo, TermsInfoWrapper>(value, nameof(TermsInfo)); }
	}


	public CostInfoWrapper CostInfo
	{
		get { return GetComplexProperty<CostInfo, CostInfoWrapper>(nameof(CostInfo)); }
		set { SetComplexProperty<CostInfo, CostInfoWrapper>(value, nameof(CostInfo)); }
	}


	public PaymentsInfoWrapper PaymentsInfo
	{
		get { return GetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(nameof(PaymentsInfo)); }
		set { SetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(value, nameof(PaymentsInfo)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(ProductBase model)
    {

        ProductsMainGroup = GetWrapper<ProductsMainGroup, ProductsMainGroupWrapper>(model.ProductsMainGroup);

        Equipment = GetWrapper<Equipment, EquipmentWrapper>(model.Equipment);

        TenderInfo = GetWrapper<TenderInfo, TenderInfoWrapper>(model.TenderInfo);

        OrderInfo = GetWrapper<OrderInfo, OrderInfoWrapper>(model.OrderInfo);

        DateInfo = GetWrapper<DateInfo, DateInfoWrapper>(model.DateInfo);

        TermsInfo = GetWrapper<TermsInfo, TermsInfoWrapper>(model.TermsInfo);

        CostInfo = GetWrapper<CostInfo, CostInfoWrapper>(model.CostInfo);

        PaymentsInfo = GetWrapper<PaymentsInfo, PaymentsInfoWrapper>(model.PaymentsInfo);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductBase model)
    {

      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => new OfferProductWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferProducts, model.OfferProducts);


    }

  }
}
