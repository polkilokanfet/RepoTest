using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using HVTApp.Model.Wrapper.Base;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using Prism.Commands;
using Prism.Events;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class LetterViewModelBase : LetterWrapper
    {
        private readonly IUnityContainer _container;
        

        public DelegateLogCommand SelectRequestDocumentCommand { get; private set; }
        public DelegateLogCommand ClearRequestDocumentCommand { get; }

        public DelegateLogCommand SelectAuthorCommand { get; }
        public DelegateLogCommand ClearAuthorCommand { get; }

        public DelegateLogCommand SelectSenderEmployeeCommand { get; }
        public DelegateLogCommand ClearSenderEmployeeCommand { get; }

        public DelegateLogCommand SelectRecipientEmployeeCommand { get; }
        public DelegateLogCommand ClearRecipientEmployeeCommand { get; }

        public DelegateLogCommand AddInCopyToRecipientsCommand { get; }
        public DelegateLogCommand RemoveFromCopyToRecipientsCommand { get; }

        private EmployeeEmptyWrapper _selectedCopyToRecipientsItem;
        public EmployeeEmptyWrapper SelectedCopyToRecipientsItem
        {
            get => _selectedCopyToRecipientsItem;
            set
            {
                if (Equals(_selectedCopyToRecipientsItem, value)) return;
                _selectedCopyToRecipientsItem = value;
                RaisePropertyChanged();
                RemoveFromCopyToRecipientsCommand.RaiseCanExecuteChanged();
            }
        }


        public LetterViewModelBase(IUnityContainer container, Document document) : base(container.Resolve<IUnitOfWork>(), document)
        {
            _container = container;

            SelectRequestDocumentCommand = new DelegateLogCommand(() => SelectAndSetWrapper<Document, DocumentWrapper>(UnitOfWork.Repository<Document>().GetAllAsNoTracking(), nameof(RequestDocument), RequestDocument?.Id));
            if (ClearRequestDocumentCommand == null) ClearRequestDocumentCommand = new DelegateLogCommand(() => RequestDocument = null);

            if (SelectAuthorCommand == null) SelectAuthorCommand = new DelegateLogCommand(() => SelectAndSetWrapper<Employee, EmployeeEmptyWrapper>(UnitOfWork.Repository<Employee>().GetAllAsNoTracking(), nameof(Author), Author?.Model.Id));
            if (ClearAuthorCommand == null) ClearAuthorCommand = new DelegateLogCommand(() => Author = null);

            if (SelectSenderEmployeeCommand == null) SelectSenderEmployeeCommand = new DelegateLogCommand(() => SelectAndSetWrapper<Employee, EmployeeEmptyWrapper>(UnitOfWork.Repository<Employee>().GetAllAsNoTracking(), nameof(SenderEmployee), SenderEmployee?.Model.Id));
            if (ClearSenderEmployeeCommand == null) ClearSenderEmployeeCommand = new DelegateLogCommand(() => SenderEmployee = null);

            if (SelectRecipientEmployeeCommand == null) SelectRecipientEmployeeCommand = new DelegateLogCommand(() => SelectAndSetWrapper<Employee, EmployeeEmptyWrapper>(UnitOfWork.Repository<Employee>().GetAllAsNoTracking(), nameof(RecipientEmployee), RecipientEmployee?.Model.Id));
            if (ClearRecipientEmployeeCommand == null) ClearRecipientEmployeeCommand = new DelegateLogCommand(() => RecipientEmployee = null);

            if (AddInCopyToRecipientsCommand == null) AddInCopyToRecipientsCommand = new DelegateLogCommand(() => SelectAndAddInListWrapper(UnitOfWork.Repository<Employee>().GetAllAsNoTracking(), CopyToRecipients));
            if (RemoveFromCopyToRecipientsCommand == null) RemoveFromCopyToRecipientsCommand = new DelegateLogCommand(() => CopyToRecipients.Remove(SelectedCopyToRecipientsItem), () => SelectedCopyToRecipientsItem != null);
        }

        private void SelectAndSetWrapper<TModel, TWrap>(IEnumerable<TModel> entities, string propertyName, Guid? selectedItemId = null)
            where TModel : class, IBaseEntity
            where TWrap : class, IWrapper<TModel>
        {
            //выбор сущности
            var entity = _container.Resolve<ISelectService>().SelectItem(entities, selectedItemId);
            if (entity == null) return;

            //поиск текущего значения
            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);
            var propertyValue = (TWrap)propertyInfo.GetValue(this);

            //замена текущего значения новым
            if (Equals(entity.Id, propertyValue?.Model.Id)) return;
            var item = UnitOfWork.Repository<TModel>().GetById(entity.Id);
            var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
            propertyInfo.SetValue(this, wrapper);
        }

        protected void SelectAndAddInListWrapper<TModel, TWrap>(IEnumerable<TModel> entities, IList<TWrap> list)
            where TModel : class, IBaseEntity
            where TWrap : WrapperBase<TModel>
        {
            //вычищение того, что уже есть в списке
            List<TModel> targetEntities = entities.ToList();
            list.Select(wrap => wrap.Model).ForEach(model => targetEntities.RemoveIfContainsById(model));

            var selectItems = _container.Resolve<ISelectService>().SelectItems(targetEntities);
            if (selectItems == null) return;
            foreach (var selectItem in selectItems)
            {
                var item = UnitOfWork.Repository<TModel>().GetById(selectItem.Id);
                var wrapper = (TWrap)Activator.CreateInstance(typeof(TWrap), item);
                list.Add(wrapper);
            }
        }

    }

    public class LetterViewModel : LetterViewModelBase
    {
        private readonly IFileManagerService _fileManagerService;

        public DelegateLogCommand OpenFolderCommand { get; }
        public DelegateLogCommand AddFilesCommand { get; }
        public DelegateCommand SaveCommand { get; }

        /// <summary>
        /// редактирование
        /// </summary>
        /// <param name="container"></param>
        /// <param name="document"></param>
        public LetterViewModel(IUnityContainer container, Document document) : base(container, document)
        {
            _fileManagerService = container.Resolve<IFileManagerService>();

            SaveCommand = new DelegateCommand(() =>
                {
                    this.AcceptChanges();
                    this.UnitOfWork.SaveEntity(this.Model);
                    container.Resolve<IEventAggregator>().GetEvent<AfterSaveDocumentEvent>().Publish(this.Model);
                },
                () => this.IsValid && this.IsChanged);

            this.PropertyChanged += (sender, args) => SaveCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// создание
        /// </summary>
        /// <param name="container"></param>
        /// <param name="direction"></param>
        public LetterViewModel(IUnityContainer container, DocumentDirection direction) : this(container, new Document())
        {
            var employee = new EmployeeEmptyWrapper(UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.Actual.SenderOfferEmployee.Id));
            var author = new EmployeeEmptyWrapper(UnitOfWork.Repository<Employee>().GetById(GlobalAppProperties.User.Id));
            switch (direction)
            {
                case DocumentDirection.Incoming:
                {
                    this.Author = author;
                    this.RecipientEmployee = employee;
                    break;
                }
                case DocumentDirection.Outgoing:
                {
                    this.Author = author;
                    this.SenderEmployee = employee;
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

    }
}