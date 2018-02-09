using System;
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
        private ProjectUnitGroupWrapper _selectedProjectUnitGroupWrapper;
        public ObservableCollection<ProjectUnitGroupWrapper> ProjectUnitGroups { get; } = new ObservableCollection<ProjectUnitGroupWrapper>();

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

        protected override async Task LoadOtherAsync()
        {
            var projectUnits = (await UnitOfWork.GetRepository<ProjectUnit>().GetAllAsync()).Where(x => x.ProjectId == Item.Id);
            foreach (var projectUnitGroup in projectUnits.ConvertToGroup())
            {
                var projectUnitGroupWrapper = new ProjectUnitGroupWrapper(projectUnitGroup);
                ProjectUnitGroups.Add(projectUnitGroupWrapper);
                projectUnitGroupWrapper.PropertyChanged += ProjectUnitGroupOnPropertyChanged;
            }
        }

        public ICommand EditProjectUnitGroupCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }

        protected override void InitCommands()
        {
            EditProjectUnitGroupCommand = new DelegateCommand(EditProjectUnitGroupCommand_Execute, EditProjectUnitGroupCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);
        }

        private void AddProjectUnitGroupCommand_Execute()
        {
            throw new NotImplementedException();
        }

        private async void EditProjectUnitGroupCommand_Execute()
        {
            var id = SelectedProjectUnitGroupWrapper.ProjectUnits.First().Id;
            await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup>(id);
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