using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderInfoWrapper : WrapperBase<TenderInfo>
  {
    protected TenderInfoWrapper(TenderInfo model) : base(model) { }

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
        get { return ProductMainWrapper.GetWrapper(Model.ProductMain); }
        set
        {
			var oldPropVal = ProductMain;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public CompanyWrapper ProducerWinner 
    {
        get { return CompanyWrapper.GetWrapper(Model.ProducerWinner); }
        set
        {
			var oldPropVal = ProducerWinner;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


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


    #endregion

    protected override void InitializeComplexProperties(TenderInfo model)
    {

        ProductMain = ProductMainWrapper.GetWrapper(model.ProductMain);

        ProducerWinner = CompanyWrapper.GetWrapper(model.ProducerWinner);

        CostInfo = CostInfoWrapper.GetWrapper(model.CostInfo);

    }

  }
}
