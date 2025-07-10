using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Services;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.Modules.BookRegistration.ViewModels
{
    public class BookRegistrationViewModel : ViewModelBaseCanExportToExcel
    {
        private Letter _selectedLetter;

        public ObservableCollection<Letter> Letters { get; } = new ObservableCollection<Letter>();

        public Letter SelectedLetter
        {
            get => _selectedLetter;
            set
            {
                SetProperty(ref _selectedLetter, value, () =>
                {
                    EditDocumentCommand.RaiseCanExecuteChanged();
                    PrintBlankLetterCommand.RaiseCanExecuteChanged();
                });
            }
        }

        #region Commands

        /// <summary>
        /// Создание исходящего документа
        /// </summary>
        public DelegateLogCommand CreateOutgoingDocumentCommand { get; }

        /// <summary>
        /// Создание входящего документа
        /// </summary>
        public DelegateLogCommand CreateIncomingDocumentCommand { get; }

        /// <summary>
        /// Редактирование документа
        /// </summary>
        public DelegateLogCommand EditDocumentCommand { get; }
        public DelegateLogCommand ReloadCommand { get; }

        /// <summary>
        /// Печать бланка письма
        /// </summary>
        public DelegateLogCommand PrintBlankLetterCommand { get; }
        
        #endregion

        public BookRegistrationViewModel(IUnityContainer container) : base(container)
        {
            var fileManagerService = container.Resolve<IFileManagerService>();
            ReloadCommand = new DelegateLogCommand(Load);

            CreateOutgoingDocumentCommand = new DelegateLogCommand(() =>
            {
                var documentViewModel = new LetterViewModel(container, DocumentDirection.Outgoing);
                container.Resolve<IDialogService>().Show(documentViewModel, "Регистрация исходящего письма");
            });

            CreateIncomingDocumentCommand = new DelegateLogCommand(() =>
            {
                var documentViewModel = new LetterViewModel(container, DocumentDirection.Incoming);
                container.Resolve<IDialogService>().Show(documentViewModel, "Регистрация входящего письма");
            });

            EditDocumentCommand = new DelegateLogCommand(
                () =>
                {
                    var document = SelectedLetter.Entity;
                    var documentViewModel = new LetterViewModel(container, document);
                    container.Resolve<IDialogService>().Show(documentViewModel, $"Редактирование письма №{document.RegNumber}");
                },
                () => SelectedLetter != null);

            PrintBlankLetterCommand = new DelegateLogCommand(
                () =>
                {
                    var path = fileManagerService.GetLettersDefaultStoragePath();
                    Container.Resolve<IPrintBlankLetterService>().PrintBlankLetter(SelectedLetter.Entity, path);
                },
                () => SelectedLetter != null && SelectedLetter.Entity.Direction == DocumentDirection.Outgoing);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveDocumentEvent>().Subscribe(document =>
            {
                if(Letters.ContainsById(document))
                    Letters.Single(letter => letter.Entity.Id == document.Id).Refresh(document);
                else
                    Letters.Insert(0, new Letter(document));
            });

            this.Load();
        }

        public void Load()
        {
            var unitOfWork = Container.Resolve<IUnitOfWork>();
            var letters = ((IDocumentRepository)unitOfWork.Repository<Document>())
                .GetAllOfCurrentUser()
                .OrderByDescending(document => document.Date)
                .ThenByDescending(document => document.Number)
                .Select(document => new Letter(document))
                .ToList();

            Letters.Clear();
            Letters.AddRange(letters);
        }
    }
}
