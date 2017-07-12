using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    private DocumentWrapper() : base(new Document()) { }
    private DocumentWrapper(Document model) : base(model) { }



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

	public DocumentWrapper RequestDocument 
    {
        get { return GetComplexProperty<DocumentWrapper, Document>(Model.RequestDocument); }
        set { SetComplexProperty<DocumentWrapper, Document>(RequestDocument, value); }
    }

    public DocumentWrapper RequestDocumentOriginalValue { get; private set; }
    public bool RequestDocumentIsChanged => GetIsChanged(nameof(RequestDocument));


	public EmployeeWrapper Author 
    {
        get { return GetComplexProperty<EmployeeWrapper, Employee>(Model.Author); }
        set { SetComplexProperty<EmployeeWrapper, Employee>(Author, value); }
    }

    public EmployeeWrapper AuthorOriginalValue { get; private set; }
    public bool AuthorIsChanged => GetIsChanged(nameof(Author));


	public EmployeeWrapper SenderEmployee 
    {
        get { return GetComplexProperty<EmployeeWrapper, Employee>(Model.SenderEmployee); }
        set { SetComplexProperty<EmployeeWrapper, Employee>(SenderEmployee, value); }
    }

    public EmployeeWrapper SenderEmployeeOriginalValue { get; private set; }
    public bool SenderEmployeeIsChanged => GetIsChanged(nameof(SenderEmployee));


	public EmployeeWrapper RecipientEmployee 
    {
        get { return GetComplexProperty<EmployeeWrapper, Employee>(Model.RecipientEmployee); }
        set { SetComplexProperty<EmployeeWrapper, Employee>(RecipientEmployee, value); }
    }

    public EmployeeWrapper RecipientEmployeeOriginalValue { get; private set; }
    public bool RecipientEmployeeIsChanged => GetIsChanged(nameof(RecipientEmployee));


	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return GetComplexProperty<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(Model.RegistrationDetailsOfSender); }
        set { SetComplexProperty<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(RegistrationDetailsOfSender, value); }
    }

    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfSenderOriginalValue { get; private set; }
    public bool RegistrationDetailsOfSenderIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSender));


	public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return GetComplexProperty<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(Model.RegistrationDetailsOfRecipient); }
        set { SetComplexProperty<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(RegistrationDetailsOfRecipient, value); }
    }

    public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipientOriginalValue { get; private set; }
    public bool RegistrationDetailsOfRecipientIsChanged => GetIsChanged(nameof(RegistrationDetailsOfRecipient));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<EmployeeWrapper> CopyToRecipients { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        RequestDocument = GetWrapper<DocumentWrapper, Document>(Model.RequestDocument);

        Author = GetWrapper<EmployeeWrapper, Employee>(Model.Author);

        SenderEmployee = GetWrapper<EmployeeWrapper, Employee>(Model.SenderEmployee);

        RecipientEmployee = GetWrapper<EmployeeWrapper, Employee>(Model.RecipientEmployee);

        RegistrationDetailsOfSender = GetWrapper<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(Model.RegistrationDetailsOfSender);

        RegistrationDetailsOfRecipient = GetWrapper<DocumentsRegistrationDetailsWrapper, DocumentsRegistrationDetails>(Model.RegistrationDetailsOfRecipient);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.CopyToRecipients == null) throw new ArgumentException("CopyToRecipients cannot be null");
      CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.CopyToRecipients.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(CopyToRecipients, Model.CopyToRecipients);


    }

  }
}
