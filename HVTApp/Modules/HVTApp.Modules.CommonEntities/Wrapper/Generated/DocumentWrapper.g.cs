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
	private DocumentWrapper _fieldRequestDocument;
	public DocumentWrapper RequestDocument 
    {
        get { return _fieldRequestDocument ; }
        set
        {
            SetComplexValue<Document, DocumentWrapper>(_fieldRequestDocument, value);
            _fieldRequestDocument  = value;
        }
    }
	private EmployeeWrapper _fieldAuthor;
	public EmployeeWrapper Author 
    {
        get { return _fieldAuthor ; }
        set
        {
            SetComplexValue<Employee, EmployeeWrapper>(_fieldAuthor, value);
            _fieldAuthor  = value;
        }
    }
	private EmployeeWrapper _fieldSenderEmployee;
	public EmployeeWrapper SenderEmployee 
    {
        get { return _fieldSenderEmployee ; }
        set
        {
            SetComplexValue<Employee, EmployeeWrapper>(_fieldSenderEmployee, value);
            _fieldSenderEmployee  = value;
        }
    }
	private EmployeeWrapper _fieldRecipientEmployee;
	public EmployeeWrapper RecipientEmployee 
    {
        get { return _fieldRecipientEmployee ; }
        set
        {
            SetComplexValue<Employee, EmployeeWrapper>(_fieldRecipientEmployee, value);
            _fieldRecipientEmployee  = value;
        }
    }
	private DocumentsRegistrationDetailsWrapper _fieldRegistrationDetailsOfSender;
	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return _fieldRegistrationDetailsOfSender ; }
        set
        {
            SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(_fieldRegistrationDetailsOfSender, value);
            _fieldRegistrationDetailsOfSender  = value;
        }
    }
	private DocumentsRegistrationDetailsWrapper _fieldRegistrationDetailsOfRecipient;
	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return _fieldRegistrationDetailsOfRecipient ; }
        set
        {
            SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(_fieldRegistrationDetailsOfRecipient, value);
            _fieldRegistrationDetailsOfRecipient  = value;
        }
    }
    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
		if (Model.RequestDocument != null)
        {
            _fieldRequestDocument = new DocumentWrapper(Model.RequestDocument);
            RegisterComplex(RequestDocument);
        }
		if (Model.Author != null)
        {
            _fieldAuthor = new EmployeeWrapper(Model.Author);
            RegisterComplex(Author);
        }
		if (Model.SenderEmployee != null)
        {
            _fieldSenderEmployee = new EmployeeWrapper(Model.SenderEmployee);
            RegisterComplex(SenderEmployee);
        }
		if (Model.RecipientEmployee != null)
        {
            _fieldRecipientEmployee = new EmployeeWrapper(Model.RecipientEmployee);
            RegisterComplex(RecipientEmployee);
        }
		if (Model.RegistrationDetailsOfSender != null)
        {
            _fieldRegistrationDetailsOfSender = new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfSender);
            RegisterComplex(RegistrationDetailsOfSender);
        }
		if (Model.RegistrationDetailsOfRecipient != null)
        {
            _fieldRegistrationDetailsOfRecipient = new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient);
            RegisterComplex(RegistrationDetailsOfRecipient);
        }
    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => new EmployeeWrapper(e)));
      RegisterCollection(CopyToRecipients, Model.CopyToRecipients);

    }
	}
}
	