using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class LetterWrapper : WrapperBase<Document>
    {
        protected IUnitOfWork UnitOfWork { get; }

        public LetterWrapper(IUnitOfWork unitOfWork, Document document) : 
            base(unitOfWork.Repository<Document>().GetById(document.Id) ?? new Document())
        {
            UnitOfWork = unitOfWork;

            if (Model.Number == null) 
                Model.Number = new DocumentNumber();
        }

        #region SimpleProperties

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date
        {
            get => Model.Date;
            set => SetValue(value);
        }
        public DateTime DateOriginalValue => GetOriginalValue<DateTime>(nameof(Date));
        public bool DateIsChanged => GetIsChanged(nameof(Date));

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment
        {
            get => Model.Comment;
            set => SetValue(value);
        }
        public string CommentOriginalValue => GetOriginalValue<string>(nameof(Comment));
        public bool CommentIsChanged => GetIsChanged(nameof(Comment));

        /// <summary>
        /// Номер в ТСЕ
        /// </summary>
        public string TceNumber
        {
            get => Model.TceNumber;
            set => SetValue(value);
        }
        public string TceNumberOriginalValue => GetOriginalValue<string>(nameof(TceNumber));
        public bool TceNumberIsChanged => GetIsChanged(nameof(TceNumber));

        #endregion

        #region ComplexProperties

        /// <summary>
        /// Запрос
        /// </summary>
        public DocumentWrapper RequestDocument
        {
            get => GetWrapper<DocumentWrapper>();
            set => SetComplexValue<Document, DocumentWrapper>(RequestDocument, value);
        }

        /// <summary>
        /// Автор
        /// </summary>
        public EmployeeEmptyWrapper Author
        {
            get => GetWrapper<EmployeeEmptyWrapper>();
            set => SetComplexValue<Employee, EmployeeEmptyWrapper>(Author, value);
        }

        /// <summary>
        /// Отправитель
        /// </summary>
        public EmployeeEmptyWrapper SenderEmployee
        {
            get => GetWrapper<EmployeeEmptyWrapper>();
            set => SetComplexValue<Employee, EmployeeEmptyWrapper>(SenderEmployee, value);
        }

        /// <summary>
        /// Получатель
        /// </summary>
        public EmployeeEmptyWrapper RecipientEmployee
        {
            get => GetWrapper<EmployeeEmptyWrapper>();
            set => SetComplexValue<Employee, EmployeeEmptyWrapper>(RecipientEmployee, value);
        }

        /// <summary>
        /// Рег.данные получателя
        /// </summary>
        public DocumentsRegistrationDetailsWrapper RegistrationDetailsOfRecipient
        {
            get => GetWrapper<DocumentsRegistrationDetailsWrapper>();
            set => SetComplexValue<DocumentsRegistrationDetails, DocumentsRegistrationDetailsWrapper>(RegistrationDetailsOfRecipient, value);
        }

        #endregion

        #region CollectionProperties

        /// <summary>
        /// Копия
        /// </summary>
        public IValidatableChangeTrackingCollection<EmployeeEmptyWrapper> CopyToRecipients { get; private set; }

        #endregion

        public override void InitializeComplexProperties()
        {
            InitializeComplexProperty(nameof(RequestDocument), Model.RequestDocument == null ? null : new DocumentWrapper(Model.RequestDocument));
            InitializeComplexProperty(nameof(Author), Model.Author == null ? null : new EmployeeEmptyWrapper(Model.Author));
            InitializeComplexProperty(nameof(SenderEmployee), Model.SenderEmployee == null ? null : new EmployeeEmptyWrapper(Model.SenderEmployee));
            InitializeComplexProperty(nameof(RecipientEmployee), Model.RecipientEmployee == null ? null : new EmployeeEmptyWrapper(Model.RecipientEmployee));
            InitializeComplexProperty(nameof(RegistrationDetailsOfRecipient), Model.RegistrationDetailsOfRecipient == null ? null : new DocumentsRegistrationDetailsWrapper(Model.RegistrationDetailsOfRecipient));
        }

        protected override void InitializeCollectionProperties()
        {
            if (Model.CopyToRecipients == null) throw new ArgumentException($"{nameof(Model.CopyToRecipients)} cannot be null");
            CopyToRecipients = new ValidatableChangeTrackingCollection<EmployeeEmptyWrapper>(Model.CopyToRecipients.Select(e => new EmployeeEmptyWrapper(e)));
            RegisterCollection(CopyToRecipients, Model.CopyToRecipients);
        }
    }
}