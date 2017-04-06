using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductOptionalWrapper : WrapperBase<ProductOptional>
  {
    protected ProductOptionalWrapper(ProductOptional model) : base(model) { }

	public static ProductOptionalWrapper GetWrapper()
	{
		return GetWrapper(new ProductOptional());
	}

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
        get { return ProductsMainGroupWrapper.GetWrapper(Model.ProductsMainGroup); }
        set
        {
			var oldPropVal = ProductsMainGroup;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public ProductsMainGroupWrapper ProductsMainGroupOriginalValue => ProductsMainGroupWrapper.GetWrapper(GetOriginalValue<ProductsMainGroup>(nameof(ProductsMainGroup)));
    public bool ProductsMainGroupIsChanged => GetIsChanged(nameof(ProductsMainGroup));


	public EquipmentWrapper Equipment 
    {
        get { return EquipmentWrapper.GetWrapper(Model.Equipment); }
        set
        {
			var oldPropVal = Equipment;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EquipmentWrapper EquipmentOriginalValue => EquipmentWrapper.GetWrapper(GetOriginalValue<Equipment>(nameof(Equipment)));
    public bool EquipmentIsChanged => GetIsChanged(nameof(Equipment));


	public TenderInfoWrapper TenderInfo 
    {
        get { return TenderInfoWrapper.GetWrapper(Model.TenderInfo); }
        set
        {
			var oldPropVal = TenderInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TenderInfoWrapper TenderInfoOriginalValue => TenderInfoWrapper.GetWrapper(GetOriginalValue<TenderInfo>(nameof(TenderInfo)));
    public bool TenderInfoIsChanged => GetIsChanged(nameof(TenderInfo));


	public OrderInfoWrapper OrderInfo 
    {
        get { return OrderInfoWrapper.GetWrapper(Model.OrderInfo); }
        set
        {
			var oldPropVal = OrderInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public OrderInfoWrapper OrderInfoOriginalValue => OrderInfoWrapper.GetWrapper(GetOriginalValue<OrderInfo>(nameof(OrderInfo)));
    public bool OrderInfoIsChanged => GetIsChanged(nameof(OrderInfo));


	public DateInfoWrapper DateInfo 
    {
        get { return DateInfoWrapper.GetWrapper(Model.DateInfo); }
        set
        {
			var oldPropVal = DateInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public DateInfoWrapper DateInfoOriginalValue => DateInfoWrapper.GetWrapper(GetOriginalValue<DateInfo>(nameof(DateInfo)));
    public bool DateInfoIsChanged => GetIsChanged(nameof(DateInfo));


	public TermsInfoWrapper TermsInfo 
    {
        get { return TermsInfoWrapper.GetWrapper(Model.TermsInfo); }
        set
        {
			var oldPropVal = TermsInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public TermsInfoWrapper TermsInfoOriginalValue => TermsInfoWrapper.GetWrapper(GetOriginalValue<TermsInfo>(nameof(TermsInfo)));
    public bool TermsInfoIsChanged => GetIsChanged(nameof(TermsInfo));


	public CostInfoWrapper CostInfo 
    {
        get { return CostInfoWrapper.GetWrapper(Model.CostInfo); }
        set
        {
			var oldPropVal = CostInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CostInfoWrapper CostInfoOriginalValue => CostInfoWrapper.GetWrapper(GetOriginalValue<CostInfo>(nameof(CostInfo)));
    public bool CostInfoIsChanged => GetIsChanged(nameof(CostInfo));


	public PaymentsInfoWrapper PaymentsInfo 
    {
        get { return PaymentsInfoWrapper.GetWrapper(Model.PaymentsInfo); }
        set
        {
			var oldPropVal = PaymentsInfo;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public PaymentsInfoWrapper PaymentsInfoOriginalValue => PaymentsInfoWrapper.GetWrapper(GetOriginalValue<PaymentsInfo>(nameof(PaymentsInfo)));
    public bool PaymentsInfoIsChanged => GetIsChanged(nameof(PaymentsInfo));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper> ProductsOptionalGroups { get; private set; }


    public IValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }


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
      ProductsOptionalGroups = new ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper>(model.ProductsOptionalGroups.Select(e => ProductsOptionalGroupWrapper.GetWrapper(e)));
      RegisterCollection(ProductsOptionalGroups, model.ProductsOptionalGroups);


      if (model.OfferProducts == null) throw new ArgumentException("OfferProducts cannot be null");
      OfferProducts = new ValidatableChangeTrackingCollection<OfferProductWrapper>(model.OfferProducts.Select(e => OfferProductWrapper.GetWrapper(e)));
      RegisterCollection(OfferProducts, model.OfferProducts);


    }

  }
}
