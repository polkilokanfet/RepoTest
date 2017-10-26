using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class PaymentPlannedWrapper : WrapperBase<PaymentPlanned>
	{
	public PaymentPlannedWrapper(PaymentPlanned model) : base(model) { }

	

    #region SimpleProperties

    public System.Guid SalesUnitId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid SalesUnitIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SalesUnitId));
    public bool SalesUnitIdIsChanged => GetIsChanged(nameof(SalesUnitId));


    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));


    public System.String Comment
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
    public bool CommentIsChanged => GetIsChanged(nameof(Comment));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private PaymentConditionWrapper _fieldCondition;
	public PaymentConditionWrapper Condition 
    {
        get { return _fieldCondition ; }
        set
        {
            SetComplexValue<PaymentCondition, PaymentConditionWrapper>(_fieldCondition, value);
            _fieldCondition  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Condition != null)
        {
            _fieldCondition = new PaymentConditionWrapper(Model.Condition);
            RegisterComplex(Condition);
        }

    }

	}
}
	