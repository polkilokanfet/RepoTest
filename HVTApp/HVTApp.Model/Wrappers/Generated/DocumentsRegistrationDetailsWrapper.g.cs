using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class DocumentsRegistrationDetailsWrapper : WrapperBase<DocumentsRegistrationDetails>
  {
    private DocumentsRegistrationDetailsWrapper(IGetWrapper getWrapper) : base(new DocumentsRegistrationDetails(), getWrapper) { }
    private DocumentsRegistrationDetailsWrapper(DocumentsRegistrationDetails model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String RegistrationNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String RegistrationNumberOriginalValue => GetOriginalValue<System.String>(nameof(RegistrationNumber));
    public bool RegistrationNumberIsChanged => GetIsChanged(nameof(RegistrationNumber));


    public System.DateTime RegistrationDate
    {
      get { return GetValue<System.DateTime>(); }
      set { SetValue(value); }
    }
    public System.DateTime RegistrationDateOriginalValue => GetOriginalValue<System.DateTime>(nameof(RegistrationDate));
    public bool RegistrationDateIsChanged => GetIsChanged(nameof(RegistrationDate));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
