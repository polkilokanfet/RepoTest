using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Converter;
using HVTApp.UI.Events;
using HVTApp.UI.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<IUnitGroup> ProjectUnits { get; } = new ObservableCollection<IUnitGroup>();

        public IUnitGroup SelectedUnitGroup
        {
            get { return _selectedUnitGroup; }
            set
            {
                _selectedUnitGroup = value;
                InvalidateCommands();
            }
        }

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

        private IUnitGroup _selectedUnitGroup;
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
                ProjectUnits.AddRange(Item.SalesUnits.ToUnitGroups());
            else
                ProjectUnits.AddRange(Item.SalesUnits);

            OnPropertyChanged(nameof(SelectedUnitGroup));
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            //var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            //var unitGroup = new UnitGroup(new List<ProjectUnit> {projectUnit});
            //var updated = await _container.Resolve<IUpdateDetailsService>().UpdateDetails<UnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(unitGroup), UnitOfWork);
        }

        private void EditCommand_Execute()
        {
            var projectUnitGroup = (UnitGroup)SelectedUnitGroup;
            var projectUnitGroupViewModel = new UnitGroupViewModel(projectUnitGroup, Container, UnitOfWork);
            Container.Resolve<IDialogService>().ShowDialog(projectUnitGroupViewModel);
        }

        private bool EditCommand_CanExecute()
        {
            //return SelectedUnitGroup != null;
            return true;
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

            await UnitOfWork.SaveChangesAsync();

            //формируем задания на расчет
            foreach (var product in ProjectUnits.Select(x => x.Product))
            {
                await Container.Resolve<IGenerateCalculatePriceTasksService>().GenerateCalculatePriceTasks(product, DateTime.Today, Item.Id);
            }

            EventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(Item.Model);

            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }
    }
}