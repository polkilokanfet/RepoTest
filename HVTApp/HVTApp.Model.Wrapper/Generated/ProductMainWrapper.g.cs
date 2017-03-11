using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class ProductMainWrapper : WrapperBase<ProductMain>
  {
    public ProductMainWrapper(ProductMain model) : base(model) { }
    public ProductMainWrapper(ProductMain model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
		set { SetComplexProperty<ProductsMainGroup, ProductsMainGroupWrapper>(value, this.ProductsMainGroup, nameof(ProductsMainGroup)); }
	}

	public EquipmentWrapper Equipment
	{
		get { return GetComplexProperty<Equipment, EquipmentWrapper>(nameof(Equipment)); }
		set { SetComplexProperty<Equipment, EquipmentWrapper>(value, this.Equipment, nameof(Equipment)); }
	}

	public TenderInfoWrapper TenderInfo
	{
		get { return GetComplexProperty<TenderInfo, TenderInfoWrapper>(nameof(TenderInfo)); }
		set { SetComplexProperty<TenderInfo, TenderInfoWrapper>(value, this.TenderInfo, nameof(TenderInfo)); }
	}

	public OrderInfoWrapper OrderInfo
	{
		get { return GetComplexProperty<OrderInfo, OrderInfoWrapper>(nameof(OrderInfo)); }
		set { SetComplexProperty<OrderInfo, OrderInfoWrapper>(value, this.OrderInfo, nameof(OrderInfo)); }
	}

	public DateInfoWrapper DateInfo
	{
		get { return GetComplexProperty<DateInfo, DateInfoWrapper>(nameof(DateInfo)); }
		set { SetComplexProperty<DateInfo, DateInfoWrapper>(value, this.DateInfo, nameof(DateInfo)); }
	}

	public TermsInfoWrapper TermsInfo
	{
		get { return GetComplexProperty<TermsInfo, TermsInfoWrapper>(nameof(TermsInfo)); }
		set { SetComplexProperty<TermsInfo, TermsInfoWrapper>(value, this.TermsInfo, nameof(TermsInfo)); }
	}

	public CostInfoWrapper CostInfo
	{
		get { return GetComplexProperty<CostInfo, CostInfoWrapper>(nameof(CostInfo)); }
		set { SetComplexProperty<CostInfo, CostInfoWrapper>(value, this.CostInfo, nameof(CostInfo)); }
	}

	public PaymentsInfoWrapper PaymentsInfo
	{
		get { return GetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(nameof(PaymentsInfo)); }
		set { SetComplexProperty<PaymentsInfo, PaymentsInfoWrapper>(value, this.PaymentsInfo, nameof(PaymentsInfo)); }
	}

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<ProductsOptionalGroupWrapper> ProductsOptionalGroups { get; private set; }

    public ValidatableChangeTrackingCollection<OfferProductWrapper> OfferProducts { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(ProductMain model)
    {
		if (model.ProductsMainGroup != null)
		{
			if (ExistsWrappers.ContainsKey(model.ProductsMainGroup))
			{
				ProductsMainGroup = (ProductsMainGroupWrapper)ExistsWrappers[model.ProductsMainGroup];
			}
			else
			{
				ProductsMainGroup = new ProductsMainGroupWrapper(model.ProductsMainGroup, ExistsWrappers);
				//ExistsWrappers.Add(model.ProductsMainGroup, new ProductsMainGroupWrapper(model.ProductsMainGroup, ExistsWrappers));
				RegisterComplexProperty(ProductsMainGroup);
			}
		}

		if (model.Equipment != null)
		{
			if (ExistsWrappers.ContainsKey(model.Equipment))
			{
				Equipment = (EquipmentWrapper)ExistsWrappers[model.Equipment];
			}
			else
			{
				Equipment = new EquipmentWrapper(model.Equipment, ExistsWrappers);
				//ExistsWrappers.Add(model.Equipment, new EquipmentWrapper(model.Equipment, ExistsWrappers));
				RegisterComplexProperty(Equipment);
			}
		}

		if (model.TenderInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.TenderInfo))
			{
				TenderInfo = (TenderInfoWrapper)ExistsWrappers[model.TenderInfo];
			}
			else
			{
				TenderInfo = new TenderInfoWrapper(model.TenderInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.TenderInfo, new TenderInfoWrapper(model.TenderInfo, ExistsWrappers));
				RegisterComplexProperty(TenderInfo);
			}
		}

		if (model.OrderInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.OrderInfo))
			{
				OrderInfo = (OrderInfoWrapper)ExistsWrappers[model.OrderInfo];
			}
			else
			{
				OrderInfo = new OrderInfoWrapper(model.OrderInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.OrderInfo, new OrderInfoWrapper(model.OrderInfo, ExistsWrappers));
				RegisterComplexProperty(OrderInfo);
			}
		}

		if (model.DateInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.DateInfo))
			{
				DateInfo = (DateInfoWrapper)ExistsWrappers[model.DateInfo];
			}
			else
			{
				DateInfo = new DateInfoWrapper(model.DateInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.DateInfo, new DateInfoWrapper(model.DateInfo, ExistsWrappers));
				RegisterComplexProperty(DateInfo);
			}
		}

		if (model.TermsInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.TermsInfo))
			{
				TermsInfo = (TermsInfoWrapper)ExistsWrappers[model.TermsInfo];
			}
			else
			{
				TermsInfo = new TermsInfoWrapper(model.TermsInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.TermsInfo, new TermsInfoWrapper(model.TermsInfo, ExistsWrappers));
				RegisterComplexProperty(TermsInfo);
			}
		}

		if (model.CostInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.CostInfo))
			{
				CostInfo = (CostInfoWrapper)ExistsWrappers[model.CostInfo];
			}
			else
			{
				CostInfo = new CostInfoWrapper(model.CostInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.CostInfo, new CostInfoWrapper(model.CostInfo, ExistsWrappers));
				RegisterComplexProperty(CostInfo);
			}
		}

		if (model.PaymentsInfo != null)
		{
			if (ExistsWrappers.ContainsKey(model.PaymentsInfo))
			{
				PaymentsInfo = (PaymentsInfoWrapper)ExistsWrappers[model.PaymentsInfo];
			}
			else
			{
				PaymentsInfo = new PaymentsInfoWrapper(model.PaymentsInfo, ExistsWrappers);
				//ExistsWrappers.Add(model.PaymentsInfo, new PaymentsInfoWrapper(model.PaymentsInfo, ExistsWrappers));
				RegisterComplexProperty(PaymentsInfo);
			}
		}

    }
  
    protected override void InitializeCollectionComplexProperties(ProductMain model)
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
