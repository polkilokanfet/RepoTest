using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TenderWrapper : WrapperBase<Tender>
  {
    public TenderWrapper(Tender model) : base(model) { }
    public TenderWrapper(Tender model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public HVTApp.Model.TenderType Type
    {
      get { return GetValue<HVTApp.Model.TenderType>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.TenderType TypeOriginalValue => GetOriginalValue<HVTApp.Model.TenderType>(nameof(Type));
    public bool TypeIsChanged => GetIsChanged(nameof(Type));

    public System.Double Sum
    {
      get { return GetValue<System.Double>(); }
      set { SetValue(value); }
    }
    public System.Double SumOriginalValue => GetOriginalValue<System.Double>(nameof(Sum));
    public bool SumIsChanged => GetIsChanged(nameof(Sum));

    public System.DateTime DateOpen
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateOpenOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateOpen));
    public bool DateOpenIsChanged => GetIsChanged(nameof(DateOpen));

    public System.DateTime DateClose
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime DateCloseOriginalValue => GetOriginalValue<System.DateTime>(nameof(DateClose));
    public bool DateCloseIsChanged => GetIsChanged(nameof(DateClose));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public ProjectWrapper Project { get; private set; }

    public CompanyWrapper Winner { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<CompanyWrapper> Participants { get; private set; }

    public ValidatableChangeTrackingCollection<TenderInfoWrapper> TenderUnits { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(Tender model)
    {
      if (model.Project == null) throw new ArgumentException("Project cannot be null");
      if (ExistsWrappers.ContainsKey(model.Project))
      {
          Project = (ProjectWrapper)ExistsWrappers[model.Project];
      }
      else
      {
          Project = new ProjectWrapper(model.Project, ExistsWrappers);
          RegisterComplexProperty(Project);
      }

      if (model.Winner == null) throw new ArgumentException("Winner cannot be null");
      if (ExistsWrappers.ContainsKey(model.Winner))
      {
          Winner = (CompanyWrapper)ExistsWrappers[model.Winner];
      }
      else
      {
          Winner = new CompanyWrapper(model.Winner, ExistsWrappers);
          RegisterComplexProperty(Winner);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(Tender model)
    {
      if (model.Participants == null) throw new ArgumentException("Participants cannot be null");
      Participants = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.Participants.Select(e => new CompanyWrapper(e, ExistsWrappers)));
      RegisterCollection(Participants, model.Participants);

      if (model.TenderUnits == null) throw new ArgumentException("TenderUnits cannot be null");
      TenderUnits = new ValidatableChangeTrackingCollection<TenderInfoWrapper>(model.TenderUnits.Select(e => new TenderInfoWrapper(e, ExistsWrappers)));
      RegisterCollection(TenderUnits, model.TenderUnits);

    }
  }
}
