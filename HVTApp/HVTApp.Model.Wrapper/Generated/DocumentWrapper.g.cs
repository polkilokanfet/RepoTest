using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    public DocumentWrapper() : base(new Document()) { }
    public DocumentWrapper(Document model) : base(model) { }

//	public static DocumentWrapper GetWrapper()
//	{
//		return GetWrapper(new Document());
//	}
//
//	public static DocumentWrapper GetWrapper(Document model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (DocumentWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new DocumentWrapper(model);
//	}


    #region SimpleProperties
    public System.String Comment
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String CommentOriginalValue => GetOriginalValue<System.String>(nameof(Comment));
    public bool CommentIsChanged => GetIsChanged(nameof(Comment));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private DocumentWrapper _fieldRequestDocument;
	public DocumentWrapper RequestDocument 
    {
        get { return _fieldRequestDocument; }
        set
        {
			SetComplexProperty<DocumentWrapper, Document>(_fieldRequestDocument, value);
			_fieldRequestDocument = value;
        }
    }
    public DocumentWrapper RequestDocumentOriginalValue { get; private set; }
    public bool RequestDocumentIsChanged => GetIsChanged(nameof(RequestDocument));

	private EmployeeWrapper _fieldAuthor;
	public EmployeeWrapper Author 
    {
        get { return _fieldAuthor; }
        set
        {
			SetComplexProperty<EmployeeWrapper, Employee>(_fieldAuthor, value);
			_fieldAuthor = value;
        }
    }
    public EmployeeWrapper AuthorOriginalValue { get; private set; }
    public bool AuthorIsChanged => GetIsChanged(nameof(Author));

	private EmployeeWrapper _fieldSenderEmployee;
	public EmployeeWrapper SenderEmployee 
    {
        get { return _fieldSenderEmployee; }
        set
        {
			SetComplexProperty<EmployeeWrapper, Employee>(_fieldSenderEmployee, value);
			_fieldSenderEmployee = value;
        }
    }
    public EmployeeWrapper SenderEmployeeOriginalValue { get; private set; }
    public bool SenderEmployeeIsChanged => GetIsChanged(nameof(SenderEmployee));

	private EmployeeWrapper _fieldRecipientEmployee;
	public EmployeeWrapper RecipientEmployee 
    {
        get { return _fieldRecipientEmployee; }
        set
        {
			SetComplexProperty<EmployeeWrapper, Employee>(_fieldRecipientEmployee, value);
			_fieldRecipientEmployee = value;
        }
    }
    public EmployeeWrapper RecipientEmployeeOriginalValue { get; private set; }
    public bool RecipientEmployeeIsChanged => GetIsChanged(nameof(RecipientEmployee));

	private RegistrationDetailsWrapper _fieldRegistrationDetailsOfSender;
	public RegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return _fieldRegistrationDetailsOfSender; }
        set
        {
			SetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(_fieldRegistrationDetailsOfSender, value);
			_fieldRegistrationDetailsOfSender = value;
        }
    }
    public RegistrationDetailsWrapper RegistrationDetailsOfSenderOriginalValue { get; private set; }
    public bool RegistrationDetailsOfSenderIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSender));

	private RegistrationDetailsWrapper _fieldRegistrationDetailsOfRecipient;
	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return _fieldRegistrationDetailsOfRecipient; }
        set
        {
			SetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(_fieldRegistrationDetailsOfRecipient, value);
			_fieldRegistrationDetailsOfRecipient = value;
        }
    }
    public RegistrationDetailsWrapper RegistrationDetailsOfRecipientOriginalValue { get; private set; }
    public bool RegistrationDetailsOfRecipientIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipient));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Document model)
    {
        RequestDocument = GetWrapper<DocumentWrapper, Document>(model.RequestDocument);
        Author = GetWrapper<EmployeeWrapper, Employee>(model.Author);
        SenderEmployee = GetWrapper<EmployeeWrapper, Employee>(model.SenderEmployee);
        RecipientEmployee = GetWrapper<EmployeeWrapper, Employee>(model.RecipientEmployee);
        RegistrationDetailsOfSender = GetWrapper<RegistrationDetailsWrapper, RegistrationDetails>(model.RegistrationDetailsOfSender);
        RegistrationDetailsOfRecipient = GetWrapper<RegistrationDetailsWrapper, RegistrationDetails>(model.RegistrationDetailsOfRecipient);
    }
  
    protected override void InitializeCollectionComplexProperties(Document model)
    {
      if (model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.CopyToRecipients.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(CopyToRecipients, model.CopyToRecipients);

    }
  }
}
