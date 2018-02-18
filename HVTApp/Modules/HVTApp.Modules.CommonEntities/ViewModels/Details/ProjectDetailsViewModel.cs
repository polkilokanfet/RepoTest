using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.ViewModels
{
    public class ProjectUnitGroupViewModel : IDialogRequestClose
    {
        public ProjectUnitGroup ProjectUnitGroup { get; }

        public ProjectUnitGroupViewModel(ProjectUnitGroup projectUnitGroup)
        {
            ProjectUnitGroup = projectUnitGroup;
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }



    public partial class ProjectDetailsViewModel
    {
        public ObservableCollection<IProjectUnit> ProjectUnits { get; } = new ObservableCollection<IProjectUnit>();
        public IProjectUnit SelectedProjectUnit { get; set; }

        public ICommand GroupingCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddProjectUnitGroupCommand { get; private set; }

        public ICommand EditFacilityCommand { get; private set; }

        protected override void InitCommands()
        {
            GroupingCommand = new DelegateCommand(GroupingCommand_Execute);
            EditCommand = new DelegateCommand(EditCommand_Execute, EditCommand_CanExecute);
            AddProjectUnitGroupCommand = new DelegateCommand(AddProjectUnitGroupCommand_Execute);

            EditFacilityCommand = new DelegateCommand(EditFacilityCommand_Execute);
        }

        private async void EditFacilityCommand_Execute()
        {
            var facilities = await UnitOfWork.GetRepository<Facility>().GetAllAsync();
            var facilityLookup = Container.Resolve<ISelectService>().SelectItem(facilities.Select(x => new FacilityLookup(x)));
            if (facilityLookup != null)
                SelectedProjectUnit.Facility = new FacilityWrapper(facilityLookup.Entity);
        }

        private bool _isGrouping = true;
        private void GroupingCommand_Execute()
        {
            _isGrouping = !_isGrouping;
            RefreshGroups();
        }


        protected override async Task LoadOtherAsync()
        {
            await Task.Factory.StartNew(() =>
            {
                ProjectUnits.Clear();
                Item.SalesUnits.ForEach(ProjectUnits.Add);
                RefreshGroups();
            });
        }

        private void RefreshGroups()
        {
            ProjectUnits.Clear();

            if (_isGrouping)
            {
                var salesUnitsGrouped = Item.SalesUnits.GroupBy(x => x, new SalesUnitComparer(new[]
                {
                    nameof(SalesUnitWrapper.ProductId),
                    nameof(SalesUnitWrapper.FacilityId),
                    nameof(SalesUnitWrapper.Cost)
                }));

                foreach (var group in salesUnitsGrouped)
                {
                    var projectUnitsGroup = new ProjectUnitGroup(group);
                    ProjectUnits.Add(projectUnitsGroup);
                }
                return;
            }

            Item.SalesUnits.ForEach(ProjectUnits.Add);
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
            var projectUnitGroupViewModel = new ProjectUnitGroupViewModel(projectUnitGroup);
            Container.Resolve<IDialogService>().ShowDialog(projectUnitGroupViewModel);
        }

        private bool EditCommand_CanExecute()
        {
            return true;
            return SelectedProjectUnit != null;
        }

        private void ProjectUnitsGroupedOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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
            //foreach (var projectUnitWrapper in _projectUnitWrappers)
            //{
            //    var blocks = projectUnitWrapper.Product.GetBlocksWithoutActualPriceOnDate(date);
            //    foreach (var productBlockWrapper in blocks)
            //    {
            //        if (actualTasks.Any(x => x.ProductBlockId == productBlockWrapper.Id && x.PriceOnDate.IsActual(date)))
            //            continue;

            //        var task = new CalculatePriceTask
            //        {
            //            ProductBlock = productBlockWrapper.Model,
            //            PriceOnDate = date
            //        };
            //        UnitOfWork.GetRepository<CalculatePriceTask>().Add(task);
            //        result = true;
            //    }
            //}
            return result;
        }
    }
}