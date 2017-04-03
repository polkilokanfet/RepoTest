using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechLinkWrapper : WrapperBase<TechLink>
  {
    protected TechLinkWrapper(TechLink model) : base(model) { }

	public static TechLinkWrapper GetWrapper(TechLink model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TechLinkWrapper)Repository.ModelWrapperDictionary[model];

		return new TechLinkWrapper(model);
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

	private TechParameterWrapper _fieldParameter;
	public TechParameterWrapper Parameter 
    {
        get { return _fieldParameter; }
        set
        {
            if (Equals(_fieldParameter, value))
                return;

            UnRegisterComplexProperty(_fieldParameter);

            _fieldParameter = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private TechLinkWrapper _fieldParentLink;
	public TechLinkWrapper ParentLink 
    {
        get { return _fieldParentLink; }
        set
        {
            if (Equals(_fieldParentLink, value))
                return;

            UnRegisterComplexProperty(_fieldParentLink);

            _fieldParentLink = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<TechLinkWrapper> ChildLinks { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(TechLink model)
    {

        Parameter = TechParameterWrapper.GetWrapper(model.Parameter);

        ParentLink = TechLinkWrapper.GetWrapper(model.ParentLink);

    }

  
    protected override void InitializeCollectionComplexProperties(TechLink model)
    {

      if (model.ChildLinks == null) throw new ArgumentException("ChildLinks cannot be null");
      ChildLinks = new ValidatableChangeTrackingCollection<TechLinkWrapper>(model.ChildLinks.Select(e => TechLinkWrapper.GetWrapper(e)));
      RegisterCollection(ChildLinks, model.ChildLinks);


    }

  }
}
