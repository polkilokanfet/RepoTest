using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using HVTApp.DataAccess.Annotations;
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
    public class ProjectUnitGroup : IProjectUnit, INotifyPropertyChanged
    {

        public ProjectUnitGroup(IEnumerable<IProjectUnit> projectUnits)
        {
            ProjectUnits = new ObservableCollection<IProjectUnit>(projectUnits);
            var unit = ProjectUnits.First();
            Facility = unit.Facility;
            Product = unit.Product;
        }

        public ObservableCollection<IProjectUnit> ProjectUnits { get; }

        public FacilityWrapper Facility { get; set; }
        public ProductWrapper Product { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public interface IProjectUnit
    {
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
    }

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

        private bool _isGrouping = true;
        private void GroupingCommand_Execute()
        {
            _isGrouping = !_isGrouping;
            RefreshGroups();
        }


        protected override async Task LoadOtherAsync()
        {
            await Task.Run(() =>
            {
                ProjectUnits.Clear();
                Item.SalesUnits.ForEach(ProjectUnits.Add);
            });
        }

        private void RefreshGroups()
        {
            ProjectUnits.Clear();

            var salesUnits = Item.SalesUnits;
            var salesUnitsGrouped = salesUnits.GroupBy(x => x, new SalesUnitComparer(new []
            {
                nameof(SalesUnitWrapper.ProductId),
                nameof(SalesUnitWrapper.FacilityId)
            }));
            foreach (var group in salesUnitsGrouped)
            {
                var projectUnitsGroup = new ProjectUnitGroup(group);
                ProjectUnits.Add(projectUnitsGroup);
            }


            //var projectUnitsGroupedCollection = _isGrouping
            //    ? salesUnits.GroupBy(x => x, new ProjectUnitGroup.SalesUnitComparer())
            //    : new List<ProjectUnitsGrouped>(_projectUnitWrappers.Select(x => new ProjectUnitsGrouped(new List<ProjectUnitWrapper>() { x })));

            //foreach (var projectUnitsGrouped in projectUnitsGroupedCollection)
            //{
            //    ProjectUnitsGroupedCollection.Add(projectUnitsGrouped);
            //    projectUnitsGrouped.PropertyChanged += ProjectUnitsGroupedOnPropertyChanged;
            //}

            //SelectedProjectUnitsGrouped = ProjectUnitsGroupedCollection.First();
        }


        private async void AddProjectUnitGroupCommand_Execute()
        {
            //var projectUnit = new ProjectUnit {Project = Item.Model, ProjectId = Item.Model.Id};
            //var projectUnitGroup = new ProjectUnitGroup(new List<ProjectUnit> {projectUnit});
            //var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnitGroup, ProjectUnitGroupWrapper>(new ProjectUnitGroupWrapper(projectUnitGroup), UnitOfWork);
        }

        private async void EditCommand_Execute()
        {
            //var projectUnit = SelectedProjectUnitsGrouped.UnitWrappers.First();
            //var updated = await Container.Resolve<IUpdateDetailsService>().UpdateDetails<ProjectUnit>(projectUnit.Id);
            //if (updated)
            //{
            //    var unit = await UnitOfWork.GetRepository<ProjectUnit>().GetByIdAsync(projectUnit.Id);

            //    SelectedProjectUnitsGrouped.Facility = new FacilityWrapper(unit.Facility);
            //    SelectedProjectUnitsGrouped.Product = new ProductWrapper(unit.Product);
            //}
        }

        private bool EditCommand_CanExecute()
        {
            //return SelectedProjectUnitsGrouped != null;
            return true;
        }

        private void ProjectUnitsGroupedOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        //protected override bool SaveCommand_CanExecute()
        //{
        //    return base.SaveCommand_CanExecute() ||
        //           (_projectUnitWrappers.Any(x => x.IsChanged) && _projectUnitWrappers.All(x => x.IsValid));
        //}

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