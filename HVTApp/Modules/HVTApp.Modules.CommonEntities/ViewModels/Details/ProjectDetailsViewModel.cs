using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
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
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }
        protected override void InitCommands()
        {
            GroupingCommand = new DelegateCommand(GroupingCommand_Execute);
            EditCommand = new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);
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
                ((DelegateCommand)EditCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        private List<ProjectUnitWrapper> _projectUnitWrappers;

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
                projectUnitGroupWrapper.PropertyChanged -= ProjectUnitsGroupedOnPropertyChanged;
            }
            ProjectUnitsGroupedCollection.Clear();

            var projectUnitsGroupedCollection = _isGrouping
                ? _projectUnitWrappers.ConvertToGroup()
                : new List<ProjectUnitsGrouped>(_projectUnitWrappers.Select(x => new ProjectUnitsGrouped(new List<ProjectUnitWrapper>() {x})));

            foreach (var projectUnitsGrouped in projectUnitsGroupedCollection)
            {
                ProjectUnitsGroupedCollection.Add(projectUnitsGrouped);
                projectUnitsGrouped.PropertyChanged += ProjectUnitsGroupedOnPropertyChanged;
            }

            SelectedProjectUnitsGrouped = ProjectUnitsGroupedCollection.First();
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            //var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            //var projectUnitGroup = new ProjectUnitGroup(new List<ProjectUnit> {projectUnit});
            //var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(projectUnitGroup), UnitOfWork);
        }

        private async void EditCommand_Execute()
        {
            var projectUnit = SelectedProjectUnitsGrouped.UnitWrappers.First();
            var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnit>(projectUnit.Id);
            if (updated)
            {
                var unit = await UnitOfWork.GetRepository<ProjectUnit>().GetByIdAsync(projectUnit.Id);
                SelectedProjectUnitsGrouped.Facility = new FacilityWrapper(unit.Facility);
                SelectedProjectUnitsGrouped.Product = new ProductWrapper(unit.Product);
            }
        }

        private bool EditCommand_CanExecute()
        {
            return SelectedProjectUnitsGrouped != null;
        }

        private void ProjectUnitsGroupedOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        protected override bool SaveCommand_CanExecute()
        {
            return base.SaveCommand_CanExecute() ||
                   (_projectUnitWrappers.Any(x => x.IsChanged) && _projectUnitWrappers.All(x => x.IsValid));
        }

        protected override async void SaveCommand_Execute()
        {
            if (await UnitOfWork.GetRepository<Project>().GetByIdAsync(Item.Model.Id) == null)
                UnitOfWork.GetRepository<Project>().Add(Item.Model);
            Item.AcceptChanges();
            await GenerateCalculatePriceTasks(DateTime.Today);

            await UnitOfWork.SaveChangesAsync();

            EventAggregator.GetEvent<AfterSaveProjectEvent>().Publish(Item.Model);

            OnCloseRequested(new DialogRequestCloseEventArgs(true));
        }

        private async Task<bool> GenerateCalculatePriceTasks(DateTime date)
        {
            var result = false;

            var actualTasks = (await UnitOfWork.GetRepository<CalculatePriceTask>().GetAllAsync()).Where(x => x.IsActual).ToList();
            foreach (var projectUnitWrapper in _projectUnitWrappers)
            {
                var blocks = projectUnitWrapper.Product.GetBlocksWithoutActualPriceOnDate(date);
                foreach (var productBlockWrapper in blocks)
                {
                    if (actualTasks.Any(x => x.ProductBlockId == productBlockWrapper.Id && x.PriceOnDate.IsActual(date)))
                        continue;

                    var task = new CalculatePriceTask
                    {
                        ProductBlock = productBlockWrapper.Model,
                        PriceOnDate = date
                    };
                    UnitOfWork.GetRepository<CalculatePriceTask>().Add(task);
                    result = true;
                }
            }
            return result;
        }
    }
}