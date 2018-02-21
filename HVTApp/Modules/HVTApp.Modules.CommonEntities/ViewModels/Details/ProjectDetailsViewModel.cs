using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<IProjectUnit> ProjectUnits { get; } = new ObservableCollection<IProjectUnit>();
        public IProjectUnit SelectedProjectUnit { get; set; }

        public ICommand GroupingCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }

        protected override void InitCommands()
        {
            GroupingCommand = new DelegateCommand(GroupingCommand_Execute);
            EditCommand = new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);
        }

        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                ProjectUnits.Clear();
                ProjectUnits.AddRange(Item.SalesUnits);
                RefreshGroups();
            });
        }


        private bool _isGrouping = true;
        private void GroupingCommand_Execute()
        {
            _isGrouping = !_isGrouping;
            RefreshGroups();
        }

        private void RefreshGroups()
        {
            ProjectUnits.Clear();

            if (_isGrouping)
                ProjectUnits.AddRange(Item.SalesUnits.ToProjectUnitGroups());
            else
                ProjectUnits.AddRange(Item.SalesUnits);

            OnPropertyChanged(nameof(SelectedProjectUnit));
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            //var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            //var projectUnitGroup = new ProjectUnitGroup(new List<ProjectUnit> {projectUnit});
            //var updated = await _container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(projectUnitGroup), UnitOfWork);
        }

        private void EditCommand_Execute()
        {
            var projectUnitGroup = (ProjectUnitGroup)SelectedProjectUnit;
            var projectUnitGroupViewModel = new ProjectUnitGroupViewModel(projectUnitGroup, Container, UnitOfWork);
            Container.Resolve<IDialogService>().ShowDialog(projectUnitGroupViewModel);
        }

        private bool EditCommand_CanExecute()
        {
            return true;
            return SelectedProjectUnit != null;
        }

        private void InvalidateCommands()
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override async void SaveCommand_Execute()
        {
            if (await UnitOfWork.GetRepository<Project>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.GetRepository<Project>().Add(Item.Model);
            Item.AcceptChanges();

            foreach (var product in ProjectUnits.Select(x => x.Product))
            {
                await Container.Resolve<IGenerateCalculatePriceTasksService>()
            }
            await GenerateCalculatePriceTasks(DateTime.Today);

            await UnitOfWork.SaveChangesAsync();

            EventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(Item.Model);

            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }
    }
}