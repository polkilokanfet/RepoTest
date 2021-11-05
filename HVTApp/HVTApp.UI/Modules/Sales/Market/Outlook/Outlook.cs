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
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class Outlook
    {
        public ObservableCollection<MessageOutlook> Messages { get; } = new ObservableCollection<MessageOutlook>();
        public MessageOutlook SelectedMessage { get; set; }
        public ICommand OpenMessageCommand { get; }

        public Outlook(Market2ViewModel market2ViewModel, IUnityContainer container)
        {
            var messagesOutlookService = container.Resolve<IMessagesOutlookService>();

            market2ViewModel.SelectedProjectItemChanged +=
                projectItem =>
                {
                    Messages.Clear();

                    if (projectItem == null)
                        return;

                    List<MessageOutlook> messages = new List<MessageOutlook>();

                    Task.Run(
                        () =>
                        {
                            var projectPath = PathGetter.GetPath(projectItem.Project);
                            messages = messagesOutlookService
                                .GetOutlookMessages(Path.Combine(projectPath, PathGetter.CorrespondenceFolderName))
                                .OrderByDescending(messageOutlook => messageOutlook.SentOnDate)
                                .ToList();
                        }).Await(
                        () =>
                        {
                            Messages.AddRange(messages);
                        },
                        exception =>
                        {
                            container.Resolve<IMessageService>().ShowOkMessageDialog(exception.GetType().FullName, exception.Message);
                        });
                };

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
    }
}