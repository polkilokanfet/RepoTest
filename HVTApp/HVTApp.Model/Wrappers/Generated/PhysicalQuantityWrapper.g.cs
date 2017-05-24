using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PhysicalQuantityWrapper : WrapperBase<PhysicalQuantity>
  {
    public PhysicalQuantityWrapper() : base(new PhysicalQuantity(), new Dictionary<IBaseEntity, object>()) { }
    public PhysicalQuantityWrapper(PhysicalQuantity model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public PhysicalQuantityWrapper(PhysicalQuantity model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<MeasureWrapper> Measures { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties(PhysicalQuantity model)
    {

      if (model.Measures == null) throw new ArgumentException("Measures cannot be null");
      Measures = new ValidatableChangeTrackingCollection<MeasureWrapper>(model.Measures.Select(e => GetWrapper<MeasureWrapper, Measure>(e)));
      RegisterCollection(Measures, model.Measures);


    }

  }
}
