using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Market.Items;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class Outlook
    {
        private readonly Market2ViewModel _market2ViewModel;
        private readonly IMessagesOutlookService _messagesOutlookService;
        private readonly IMessageService _messageService;

        public ObservableCollection<MessageOutlook> Messages { get; } = new ObservableCollection<MessageOutlook>();
        public MessageOutlook SelectedMessage { get; set; }
        public ICommand OpenMessageCommand { get; }

        public Outlook(Market2ViewModel market2ViewModel, IUnityContainer container)
        {
            _market2ViewModel = market2ViewModel;
            _messagesOutlookService = container.Resolve<IMessagesOutlookService>();
            _messageService = container.Resolve<IMessageService>();

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
                if (originalMessageList.Where(x => !Equals(x.FilePath, message.FilePath)).Any(x => message.Equals(x)))
                {
                    //удаляем его
                    originalMessageList.Remove(message);
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
            var path = Path.Combine(PathGetter.GetPath(project), PathGetter.CorrespondenceFolderName);
            return _messagesOutlookService.GetOutlookMessages(path);
        }
    }
}