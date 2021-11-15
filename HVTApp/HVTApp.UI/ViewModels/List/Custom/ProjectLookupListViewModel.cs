using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectLookupListViewModel
    {
        public DelegateLogCommand CopyAttachmentsCommand { get; private set; }
        protected override void InitSpecialCommands()
        {
            CopyAttachmentsCommand = new DelegateLogCommand(
                () =>
                {
                    IEventServiceClient eventServiceClient = Container.Resolve<IEventServiceClient>();
                    IMessageService messageService = Container.Resolve<IMessageService>();

                    if (SelectedItems != null && SelectedItems.Any())
                    {
                        var selectedProjects = SelectedItems.ToList();
                        var managers = selectedProjects.Select(project => project.Manager).Distinct().ToList();
                        var managersOffline = new List<User>();
                        foreach (var manager in managers)
                        {
                            if (eventServiceClient.UserConnected(manager.Id) == false)
                                managersOffline.Add(manager);
                        }

                        if (managersOffline.Any())
                        {
                            var dr = messageService.ShowYesNoMessageDialog("Некоторые менеджеры не подключены", $"{managersOffline.ToStringEnum()} не подключены. Продолжаем?");
                            if (dr != MessageDialogResult.Yes) return;
                        }

                        managersOffline.ForEach(user => managers.Remove(user));
                        if (managers.Any())
                        {
                            using (var fdb = new FolderBrowserDialog())
                            {
                                var dialogResult = fdb.ShowDialog();
                                if (dialogResult == DialogResult.OK && !string.IsNullOrWhiteSpace(fdb.SelectedPath))
                                {
                                    var selectedDirectoryPath = fdb.SelectedPath;

                                    foreach (var selectedProject in selectedProjects)
                                    {
                                        if (!managers.Contains(selectedProject.Manager)) continue;

                                        var targetDirectoryPath = Path.Combine(selectedDirectoryPath, selectedProject.Id.ToString().Replace("-", string.Empty));
                                        this.Container.Resolve<IFileManagerService>().CreateDirectoryPathIfNotExists(targetDirectoryPath);
                                        eventServiceClient.CopyProjectAttachmentsRequest(selectedProject.Manager.Id, selectedProject.Id, targetDirectoryPath);
                                    }

                                    messageService.ShowOkMessageDialog("Info", $"Started copy proccess to: {selectedDirectoryPath}");
                                }
                            }
                        }
                    }
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup => CopyAttachmentsCommand.RaiseCanExecuteChanged();

        }
    }
}