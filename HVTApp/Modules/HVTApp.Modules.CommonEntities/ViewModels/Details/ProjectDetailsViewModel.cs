using System;
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
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<ProjectUnitGroupWrapper> ProjectUnitGroups { get; } = new ObservableCollection<ProjectUnitGroupWrapper>();

        public ICommand EditProjectUnitGroupCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }
        protected override void InitCommands()
        {
            EditProjectUnitGroupCommand = new DelegateCommand(EditProjectUnitGroupCommand_Execute, EditProjectUnitGroupCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);
        }

        private ProjectUnitGroupWrapper _selectedProjectUnitGroupWrapper;
        public ProjectUnitGroupWrapper SelectedProjectUnitGroupWrapper
        {
            get { return _selectedProjectUnitGroupWrapper; }
            set
            {
                if (Equals(_selectedProjectUnitGroupWrapper, value)) return;
                _selectedProjectUnitGroupWrapper = value;
                ((DelegateCommand)EditProjectUnitGroupCommand).RaiseCanExecuteChanged();
            }
        }

        private List<ProjectUnit> _projectUnits;

        protected override async Task LoadOtherAsync()
        {
            _projectUnits = (await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsync()).Where(x => x.ProjectId == Item.Id).ToList();
            GetGroups();
        }

        private void GetGroups()
        {
            foreach (var projectUnitGroupWrapper in ProjectUnitGroups)
            {
                projectUnitGroupWrapper.PropertyChanged -= ProjectUnitGroupOnPropertyChanged;
            }
            ProjectUnitGroups.Clear();

            foreach (var projectUnitGroup in _projectUnits.ConvertToGroup())
            {
                var projectUnitGroupWrapper = new ProjectUnitGroupWrapper(projectUnitGroup);
                ProjectUnitGroups.Add(projectUnitGroupWrapper);
                projectUnitGroupWrapper.PropertyChanged += ProjectUnitGroupOnPropertyChanged;
            }
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            var projectUnitGroup = new ProjectUnitGroup(new List<ProjectUnit> {projectUnit});
            var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(projectUnitGroup), UnitOfWork);
        }

        private async void EditProjectUnitGroupCommand_Execute()
        {
            var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(SelectedProjectUnitGroupWrapper, UnitOfWork);
            if (updated)
            {
                foreach (var projectUnitWrapper in SelectedProjectUnitGroupWrapper.ProjectUnits.AddedItems)
                {
                    _projectUnits.Add(projectUnitWrapper.Model);
                    UnitOfWork.GetRepository<ProjectUnit>().Add(projectUnitWrapper.Model);
                }
                foreach (var projectUnitWrapper in SelectedProjectUnitGroupWrapper.ProjectUnits.RemovedItems)
                {
                    _projectUnits.Remove(projectUnitWrapper.Model);
                    UnitOfWork.GetRepository<ProjectUnit>().Delete(projectUnitWrapper.Model);
                }

                GetGroups();
            }
            else
            {
                SelectedProjectUnitGroupWrapper.RejectChanges();
            }
        }

        private bool EditProjectUnitGroupCommand_CanExecute()
        {
            return SelectedProjectUnitGroupWrapper != null;
        }

        private void ProjectUnitGroupOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override bool SaveCommand_CanExecute()
        {
            return base.SaveCommand_CanExecute() ||
                   (ProjectUnitGroups.Any(x => x.IsChanged) && ProjectUnitGroups.All(x => x.IsValid));
        }
    }
}