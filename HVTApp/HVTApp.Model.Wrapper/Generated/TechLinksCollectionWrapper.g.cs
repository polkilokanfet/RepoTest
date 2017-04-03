using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class TechLinksCollectionWrapper : WrapperBase<TechLinksCollection>
  {
    protected TechLinksCollectionWrapper(TechLinksCollection model) : base(model) { }

	public static TechLinksCollectionWrapper GetWrapper(TechLinksCollection model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (TechLinksCollectionWrapper)Repository.ModelWrapperDictionary[model];

		return new TechLinksCollectionWrapper(model);
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


    #region GetProperties

    public System.Int32 Count => GetValue<System.Int32>(); 


    public System.Boolean IsReadOnly => GetValue<System.Boolean>(); 


    #endregion

  }
}
