using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductOptionalWrapper : WrapperBase<ProductOptional>
  {
    public ProductOptionalWrapper(ProductOptional model) : base(model) { }
    public ProductOptionalWrapper(ProductOptional model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static ProductOptionalWrapper GetWrapper(ProductOptional model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (ProductOptionalWrapper)Repository.ModelWrapperDictionary[model];

		return new ProductOptionalWrapper(model);
	}



    #region SimpleProperties

    public System.Boolean InCoast
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean InCoastOriginalValue => GetOriginalValue<System.Boolean>(nameof(InCoast));
    public bool InCoastIsChanged => GetIsChanged(nameof(InCoast));


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

    public ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper> ProductsOptionalGroups { get; private set; }


    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(ProductOptional model)
    {

        ProductsMainGroup = ProductsMainGroupWrapper.GetWrapper(model.ProductsMainGroup);

        Equipment = EquipmentWrapper.GetWrapper(model.Equipment);

        TenderInfo = TenderInfoWrapper.GetWrapper(model.TenderInfo);

        OrderInfo = OrderInfoWrapper.GetWrapper(model.OrderInfo);

        DateInfo = DateInfoWrapper.GetWrapper(model.DateInfo);

        TermsInfo = TermsInfoWrapper.GetWrapper(model.TermsInfo);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

        PaymentsInfo = PaymentsInfoWrapper.GetWrapper(model.PaymentsInfo);

    }

  
    protected override void InitializeCollectionComplexProperties(ProductOptional model)
    {

      if (model.ProductsOptionalGroups == null) throw new ArgumentException("ProductsOptionalGroups cannot be null");
      ProductsOptionalGroups = new ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper>(model.ProductsOptionalGroups.Select(e => new ProductsOptionalGroupWrapper(e, ExistsWrappers)));
      RegisterCollection(ProductsOptionalGroups, model.ProductsOptionalGroups);


      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => new OfferProductWrapper(e, ExistsWrappers)));
      RegisterCollection(OfferProducts, model.OfferProducts);


    }

  }
}
