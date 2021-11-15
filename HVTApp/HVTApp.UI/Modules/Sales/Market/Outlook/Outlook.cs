using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class Outlook : BindableBase
    {
        private readonly Market2ViewModel _market2ViewModel;
        private readonly IMessagesOutlookService _messagesOutlookService;
        private readonly IMessageService _messageService;
        private readonly IFileManagerService _fileManagerService;
        private MessageOutlook _selectedMessage;

        public ObservableCollection<MessageOutlook> Messages { get; } = new ObservableCollection<MessageOutlook>();

        public MessageOutlook SelectedMessage
        {
            get => _selectedMessage;
            set
            {
                if (Equals(_selectedMessage, value)) return;

                _selectedMessage = value;
                RaisePropertyChanged();
                SelectedMessageChanged?.Invoke(value);
            }
        }

        public ICommand OpenMessageCommand { get; }
        public ICommand DeleteMessageCommand { get; }

        public event Action<MessageOutlook> SelectedMessageChanged;

        public Outlook(Market2ViewModel market2ViewModel, IUnityContainer container)
        {
            _market2ViewModel = market2ViewModel;
            _messagesOutlookService = container.Resolve<IMessagesOutlookService>();
            _messageService = container.Resolve<IMessageService>();
            _fileManagerService = container.Resolve<IFileManagerService>();

            market2ViewModel.SelectedProjectItemChanged += OnMarket2ViewModelOnSelectedProjectItemChanged;

            OpenMessageCommand = new DelegateLogCommand(
                () =>
                {
                    if (SelectedMessage == null)
                        return;

                    try
                    {
                        Process.Start(SelectedMessage.FilePath);
                    }
                    catch (Exception e)
                    {
                        container.Resolve<IMessageService>().ShowOkMessageDialog(e.GetType().ToString(), e.PrintAllExceptions());
                    }

                });

            DeleteMessageCommand = new DelegateLogCommand(
                () =>
                {
                    if (SelectedMessage == null)
                        return;

                    var dr = _messageService.ShowYesNoMessageDialog("Удаление", "Вы хотите удалить выделенное сообщение?", defaultYes:true);
                    if (dr == MessageDialogResult.Yes)
                    {
                        DeleteMessage(SelectedMessage);
                        Messages.Remove(SelectedMessage);
                        SelectedMessage = null;
                    }
                });


        }

        /// <summary>
        /// Удаление дублирующих сообщений
        /// </summary>
        public void DeleteDuplicateMessages()
        {
            List<MessageOutlook> originalMessageList = GetMessages(_market2ViewModel.SelectedProjectItem.Project).ToList();
            foreach (var message in originalMessageList.ToList())
            {
                //если встречается дубликат
                if (originalMessageList.Where(messageOutlook => !Equals(messageOutlook.FilePath, message.FilePath)).Any(x => message.Equals(x)))
                {
                    //удаляем его
                    originalMessageList.Remove(message);
                    DeleteMessage(message);
                }
            }

            OnMarket2ViewModelOnSelectedProjectItemChanged(_market2ViewModel.SelectedProjectItem);
        }

        void OnMarket2ViewModelOnSelectedProjectItemChanged(ProjectItem projectItem)
        {
            Messages.Clear();

            if (projectItem == null) return;

            IEnumerable<MessageOutlook> messages = new List<MessageOutlook>();

            Task.Run(
                () =>
                {
                    messages = GetMessages(projectItem.Project).OrderByDescending(messageOutlook => messageOutlook.SentOnDate);
                }).Await(
                () =>
                {
                    Messages.AddRange(messages);
                },
                exception =>
                {
                    _messageService.ShowOkMessageDialog(exception.GetType().FullName, exception.Message);
                });
        }

        private IEnumerable<MessageOutlook> GetMessages(Project project)
        {
            var path = _fileManagerService.GetProjectCorrespondenceFolderName(project);
            return _messagesOutlookService.GetOutlookMessages(path);
        }

        private void DeleteMessage(MessageOutlook message)
        {
            try
            {
                File.Delete(message.FilePath);
            }
            catch (IOException e)
            {
                _messageService.ShowOkMessageDialog("Exception", e.Message);
            }
        }
    }
}