using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class MeasureWrapper : WrapperBase<Measure>
  {
    public MeasureWrapper() : base(new Measure(), new Dictionary<IBaseEntity, object>()) { }
    public MeasureWrapper(Measure model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public MeasureWrapper(Measure model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public System.String FullName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
    public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

    public System.String ShortName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
    public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public PhysicalQuantityWrapper PhysicalQuantity 
    {
        get { return GetComplexProperty<PhysicalQuantityWrapper, PhysicalQuantity>(Model.PhysicalQuantity); }
        set { SetComplexProperty<PhysicalQuantityWrapper, PhysicalQuantity>(PhysicalQuantity, value); }
    }

    public PhysicalQuantityWrapper PhysicalQuantityOriginalValue { get; private set; }
    public bool PhysicalQuantityIsChanged => GetIsChanged(nameof(PhysicalQuantity));

    #endregion
    protected override void InitializeComplexProperties(Measure model)
    {
        PhysicalQuantity = GetWrapper<PhysicalQuantityWrapper, PhysicalQuantity>(model.PhysicalQuantity);
    }
  }
}
