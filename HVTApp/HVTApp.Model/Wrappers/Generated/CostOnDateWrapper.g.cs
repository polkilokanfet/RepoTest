using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class CostOnDateWrapper : WrapperBase<CostOnDate>
  {
    private CostOnDateWrapper(IGetWrapper getWrapper) : base(new CostOnDate(), getWrapper) { }
    private CostOnDateWrapper(CostOnDate model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.DateTime Date
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOriginalValue => GetOriginalValue<System.DateTime>(nameof(Date));
    public bool DateIsChanged => GetIsChanged(nameof(Date));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public CostWrapper Cost 
    {
        get { return GetComplexProperty<CostWrapper, Cost>(Model.Cost); }
        set { SetComplexProperty<CostWrapper, Cost>(Cost, value); }
    }

    public CostWrapper CostOriginalValue { get; private set; }
    public bool CostIsChanged => GetIsChanged(nameof(Cost));


    #endregion

    public override void InitializeComplexProperties()
    {

        Cost = GetWrapper<CostWrapper, Cost>(Model.Cost);

    }

  }
}
