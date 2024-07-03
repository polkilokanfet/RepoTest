using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectLookupListViewModel
    {
        public DelegateLogCommand ChangeManagerCommand { get; private set; }
        public DelegateLogCommand CopyAttachmentsCommand { get; private set; }
        
        protected override void InitSpecialCommands()
        {
            ChangeManagerCommand = new DelegateLogCommand(
                () =>
                {
                    var managers = UnitOfWork.Repository<User>().Find(user => user.Roles.Select(role => role.Role).Contains(Role.SalesManager));
                    var manager = Container.Resolve<ISelectService>().SelectItem(managers);
                    if (manager == null) return;

                    using (var unitOfWork = Container.Resolve<IUnitOfWork>())
                    {
                        manager = unitOfWork.Repository<User>().GetById(manager.Id);
                        var project = unitOfWork.Repository<Project>().GetById(SelectedItem.Id);
                        project.Manager = manager;

                        var salesUnits = unitOfWork.Repository<SalesUnit>().Find(salesUnit => salesUnit.Project.Id == project.Id);
                        var tasks = unitOfWork.Repository<PriceEngineeringTask>().Find(task => task.SalesUnits.Intersect(salesUnits).Any()).Distinct().ToList();
                        var tt = tasks.Select(task => task.GetPriceEngineeringTasks(unitOfWork)).Distinct();
                        foreach (var t in tt)
                        {
                            t.UserManager = manager;
                        }

                        unitOfWork.SaveChanges();
                    }
                },
                () => SelectedItem != null);

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
                            var dr = messageService.ConfirmationDialog("Некоторые менеджеры не подключены", $"{managersOffline.ToStringEnum()} не подключены. Продолжаем?");
                            if (dr == false) return;
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
                                        //eventServiceClient.CopyProjectAttachmentsRequest(selectedProject.Manager.Id, selectedProject.Id, targetDirectoryPath);
                                    }

                                    messageService.Message("Info", $"Started copy proccess to: {selectedDirectoryPath}");
                                }
                            }
                        }
                    }
                },
                () => SelectedItem != null);

            this.SelectedLookupChanged += lookup =>
            {
                ChangeManagerCommand.RaiseCanExecuteChanged();
                CopyAttachmentsCommand.RaiseCanExecuteChanged();
            };

        }
    }
}