using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectLookupListViewModel
    {
        public ICommand CopyAttachmentsCommand { get; private set; }
        protected override void InitSpecialCommands()
        {
            CopyAttachmentsCommand = new DelegateCommand(
                () =>
                {
                    IEventServiceClient eventServiceClient = Container.Resolve<IEventServiceClient>();
                    IMessageService messageService = Container.Resolve<IMessageService>();
                    if (eventServiceClient.UserConnected(SelectedItem.Manager.Id))
                    {
                        using (var fdb = new FolderBrowserDialog())
                        {
                            var dialogResult = fdb.ShowDialog();
                            if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                            {
                                var targetDirectoryPath = fdb.SelectedPath;
                                targetDirectoryPath = Path.Combine(targetDirectoryPath, SelectedItem.Id.ToString());
                                PathGetter.CreateDirectoryPathIfNotExists(targetDirectoryPath);
                                eventServiceClient.CopyProjectAttachmentsRequest(SelectedItem.Manager.Id, SelectedItem.Id, targetDirectoryPath);
                                messageService.ShowOkMessageDialog("Info", $"Started copy proccess to: {targetDirectoryPath}");
                            }
                        }
                    }
                    else
                    {
                        messageService.ShowOkMessageDialog("Attachments did not copy", "User is not connected to EventService.");
                    }
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => ((DelegateCommand)CopyAttachmentsCommand).RaiseCanExecuteChanged();

        }
    }
}