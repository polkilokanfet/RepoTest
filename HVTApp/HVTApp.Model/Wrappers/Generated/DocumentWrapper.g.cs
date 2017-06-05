using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class DocumentWrapper : WrapperBase<Document>
  {
    public DocumentWrapper() : base(new Document(), new Dictionary<IBaseEntity, object>()) { }
    public DocumentWrapper(Document model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    public DocumentWrapper(Document model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }



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


	public RegistrationDetailsWrapper RegistrationDetailsOfSender 
    {
        get { return GetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(Model.RegistrationDetailsOfSender); }
        set { SetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(RegistrationDetailsOfSender, value); }
    }

    public RegistrationDetailsWrapper RegistrationDetailsOfSenderOriginalValue { get; private set; }
    public bool RegistrationDetailsOfSenderIsChanged => GetIsChanged(nameof(RegistrationDetailsOfSender));


	public RegistrationDetailsWrapper RegistrationDetailsOfRecipient 
    {
        get { return GetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(Model.RegistrationDetailsOfRecipient); }
        set { SetComplexProperty<RegistrationDetailsWrapper, RegistrationDetails>(RegistrationDetailsOfRecipient, value); }
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
