using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
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
	public DocumentWrapper RequestDocument 
    {
        get { return GetWrapper<DocumentWrapper>(); }
        set { SetComplexValue<Document, DocumentWrapper>(RequestDocument, value); }
    }

	public EmployeeWrapper Author 
    {
        get { return GetWrapper<EmployeeWrapper>(); }
        set { SetComplexValue<Employee, EmployeeWrapper>(Author, value); }
    }

	public EmployeeWrapper SenderEmployee 
    {
        get { return GetWrapper<EmployeeWrapper>(); }
        set { SetComplexValue<Employee, EmployeeWrapper>(SenderEmployee, value); }
    }

	public EmployeeWrapper RecipientEmployee 
    {
        get { return GetWrapper<EmployeeWrapper>(); }
        set { SetComplexValue<Employee, EmployeeWrapper>(RecipientEmployee, value); }
    }

	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
        set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfSender, value); }
    }

	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return GetWrapper<DocumentsRegistrationDetailsWrapper>(); }
        set { SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<DocumentWrapper>(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));

        InitializeComplexProperty<EmployeeWrapper>(nameof(Author), Model.Author == null ? null : new EmployeeWrapper(Model.Author));

        InitializeComplexProperty<EmployeeWrapper>(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeWrapper(Model.SenderEmployee));

        InitializeComplexProperty<EmployeeWrapper>(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeWrapper(Model.RecipientEmployee));

        InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfSender), Model.RegistrationDetailsOfSender == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfSender));

        InitializeComplexProperty<DocumentsRegistrationDetailsWrapper>(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
      RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

    }
	}
}
	