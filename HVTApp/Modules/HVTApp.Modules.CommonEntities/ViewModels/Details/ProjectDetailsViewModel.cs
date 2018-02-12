using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Extantions;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<ProjectUnitsGrouped> ProjectUnitsGroupedCollection { get; } = new ObservableCollection<ProjectUnitsGrouped>();


        public ICommand GroupingCommand { get; private set; }
        public ICommand EditProjectUnitGroupCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }
        protected override void InitCommands()
        {
            GroupingCommand = new DelegateCommand(GroupingCommand_Execute);
            EditProjectUnitGroupCommand = new DelegateCommand(EditProjectUnitGroupCommand_Execute, EditProjectUnitGroupCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);
        }

        private bool _isGrouping = true;
        private void GroupingCommand_Execute()
        {
            _isGrouping = !_isGrouping;
            RefreshGroups();
        }

        private ProjectUnitsGrouped _selectedProjectUnitsGrouped;
        public ProjectUnitsGrouped SelectedProjectUnitsGrouped
        {
            get { return _selectedProjectUnitsGrouped; }
            set
            {
                if (Equals(_selectedProjectUnitsGrouped, value)) return;
                _selectedProjectUnitsGrouped = value;
                ((DelegateCommand)EditProjectUnitGroupCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private List<ProjectUnitWrapper> _projectUnitWrappers;
        private IEnumerable<ProjectUnit> ProjectUnits => _projectUnitWrappers.Select(x => x.Model);

        protected override async Task LoadOtherAsync()
        {
            _projectUnitWrappers = (await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsync())
                .Where(x => x.ProjectId == Item.Id).Select(x => new ProjectUnitWrapper(x)).ToList();
            RefreshGroups();
        }

        private void RefreshGroups()
        {
            foreach (var projectUnitGroupWrapper in ProjectUnitsGroupedCollection)
            {
                projectUnitGroupWrapper.PropertyChanged -= projectUnitsGroupedOnPropertyChanged;
            }
            ProjectUnitsGroupedCollection.Clear();

            var projectUnitsGroupedCollection = _isGrouping
                ? _projectUnitWrappers.ConvertToGroup()
                : new List<ProjectUnitsGrouped>(_projectUnitWrappers.Select(x => new ProjectUnitsGrouped(new List<ProjectUnitWrapper>() {x})));

            foreach (var projectUnitsGrouped in projectUnitsGroupedCollection)
            {
                ProjectUnitsGroupedCollection.Add(projectUnitsGrouped);
                projectUnitsGrouped.PropertyChanged += projectUnitsGroupedOnPropertyChanged;
            }

            SelectedProjectUnitsGrouped = ProjectUnitsGroupedCollection.First();
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            //var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            //var projectUnitGroup = new ProjectUnitGroup(new List<ProjectUnit> {projectUnit});
            //var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(projectUnitGroup), UnitOfWork);
        }

        private async void EditProjectUnitGroupCommand_Execute()
        {
            //var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(SelectedProjectUnitsGrouped, UnitOfWork);
            //if (updated)
            //{
            //    foreach (var projectUnitWrapper in SelectedProjectUnitsGrouped.ProjectUnits.AddedItems)
            //    {
            //        _projectUnitWrappers.Add(projectUnitWrapper.Model);
            //        UnitOfWork.GetRepository<ProjectUnit>().Add(projectUnitWrapper.Model);
            //    }
            //    foreach (var projectUnitWrapper in SelectedProjectUnitsGrouped.ProjectUnits.RemovedItems)
            //    {
            //        _projectUnitWrappers.Remove(projectUnitWrapper.Model);
            //        UnitOfWork.GetRepository<ProjectUnit>().Delete(projectUnitWrapper.Model);
            //    }
                
            //    RefreshGroups();
            //    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            //}
            //else
            //{
            //    SelectedProjectUnitsGrouped.RejectChanges();
            //}
        }

        private bool EditProjectUnitGroupCommand_CanExecute()
        {
            return SelectedProjectUnitsGrouped != null;
        }

        private void projectUnitsGroupedOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override bool SaveCommand_CanExecute()
        {
            return base.SaveCommand_CanExecute() ||
                   (_projectUnitWrappers.Any(x => x.IsChanged) && _projectUnitWrappers.All(x => x.IsValid));
        }
    }
}