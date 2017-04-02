using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderInfoWrapper : WrapperBase<TenderInfo>
  {
    public TenderInfoWrapper(TenderInfo model) : base(model) { }
    public TenderInfoWrapper(TenderInfo model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static TenderInfoWrapper GetWrapper(TenderInfo model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TenderInfoWrapper)Repository.ModelWrapperDictionary[model];

		return new TenderInfoWrapper(model);
	}



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

	public ProductMainWrapper ProductMain
	{
		get { return GetComplexProperty<ProductMain, ProductMainWrapper>(nameof(ProductMain)); }
		set { SetComplexProperty<ProductMain, ProductMainWrapper>(value, nameof(ProductMain)); }
	}


	public CompanyWrapper ProducerWinner
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(ProducerWinner)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, nameof(ProducerWinner)); }
	}


	public CostInfoWrapper CostInfo
	{
		get { return GetComplexProperty<CostInfo, CostInfoWrapper>(nameof(CostInfo)); }
		set { SetComplexProperty<CostInfo, CostInfoWrapper>(value, nameof(CostInfo)); }
	}


    #endregion

    protected override void InitializeComplexProperties(TenderInfo model)
    {

        ProductMain = ProductMainWrapper.GetWrapper(model.ProductMain);

        ProducerWinner = CompanyWrapper.GetWrapper(model.ProducerWinner);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

    }

  }
}
