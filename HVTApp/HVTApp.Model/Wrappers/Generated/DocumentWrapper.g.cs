using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    public DocumentWrapper(Document model) : base(model) { }



    #region SimpleProperties

    public System.Nullable<System.Guid> AuthorId
    {
      get { return GetValue<System.Nullable<System.Guid>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Guid> AuthorIdOriginalValue => GetOriginalValue<System.Nullable<System.Guid>>(nameof(AuthorId));
    public bool AuthorIdIsChanged => GetIsChanged(nameof(AuthorId));


    public System.Guid SenderId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid SenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(SenderId));
    public bool SenderIdIsChanged => GetIsChanged(nameof(SenderId));


    public System.Guid RecipientId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid RecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RecipientId));
    public bool RecipientIdIsChanged => GetIsChanged(nameof(RecipientId));


    public System.Guid RegistrationDetailsOfSenderId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid RegistrationDetailsOfSenderIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RegistrationDetailsOfSenderId));
    public bool RegistrationDetailsOfSenderIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSenderId));


    public System.Guid RegistrationDetailsOfRecipientId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid RegistrationDetailsOfRecipientIdOriginalValue => GetOriginalValue<System.Guid>(nameof(RegistrationDetailsOfRecipientId));
    public bool RegistrationDetailsOfRecipientIdIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipientId));


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

	public DocumentWrapper RequestDocument { get; set; }

	public EmployeeWrapper Author { get; set; }

	public EmployeeWrapper SenderEmployee { get; set; }

	public EmployeeWrapper RecipientEmployee { get; set; }

	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender { get; set; }

	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        RequestDocument = new DocumentWrapper(Model.RequestDocument);
		RegisterComplex(RequestDocument);

        Author = new EmployeeWrapper(Model.Author);
		RegisterComplex(Author);

        SenderEmployee = new EmployeeWrapper(Model.SenderEmployee);
		RegisterComplex(SenderEmployee);

        RecipientEmployee = new EmployeeWrapper(Model.RecipientEmployee);
		RegisterComplex(RecipientEmployee);

        RegistrationDetailsOfSender = new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfSender);
		RegisterComplex(RegistrationDetailsOfSender);

        RegistrationDetailsOfRecipient = new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient);
		RegisterComplex(RegistrationDetailsOfRecipient);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
      RegisterCollection(CopyToRecipients, Model.CopyToRecipients);


    }

  }
}
