using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechLinkWrapper : WrapperBase<TechLink>
  {
    public TechLinkWrapper(TechLink model) : base(model) { }
    public TechLinkWrapper(TechLink model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
    public TechParameterWrapper Parameter { get; private set; }

    public TechLinkWrapper ParentLink { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<TechLinkWrapper> ChildLinks { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(TechLink model)
    {
      if (model.Parameter == null) throw new ArgumentException("Parameter cannot be null");
      if (ExistsWrappers.ContainsKey(model.Parameter))
      {
          Parameter = (TechParameterWrapper)ExistsWrappers[model.Parameter];
      }
      else
      {
          Parameter = new TechParameterWrapper(model.Parameter, ExistsWrappers);
          RegisterComplexProperty(Parameter);
      }

      if (model.ParentLink == null) throw new ArgumentException("ParentLink cannot be null");
      if (ExistsWrappers.ContainsKey(model.ParentLink))
      {
          ParentLink = (TechLinkWrapper)ExistsWrappers[model.ParentLink];
      }
      else
      {
          ParentLink = new TechLinkWrapper(model.ParentLink, ExistsWrappers);
          RegisterComplexProperty(ParentLink);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(TechLink model)
    {
      if (model.ChildLinks == null) throw new ArgumentException("ChildLinks cannot be null");
      ChildLinks = new ValidatableChangeTrackingCollection<TechLinkWrapper>(model.ChildLinks.Select(e => new TechLinkWrapper(e, ExistsWrappers)));
      RegisterCollection(ChildLinks, model.ChildLinks);

    }
  }
}
