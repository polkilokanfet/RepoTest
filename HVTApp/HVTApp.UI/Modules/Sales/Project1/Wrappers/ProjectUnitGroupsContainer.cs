using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitGroupsContainer : BindableBase, IEnumerable<ProjectUnitGroup>, INotifyCollectionChanged
    {
        private IProjectUnit _selectedUnit;

        public IProjectUnit SelectedUnit
        {
            get => _selectedUnit;
            set => SetProperty(ref _selectedUnit, value);
        }

        private ObservableCollection<ProjectUnitGroup> SalesUnitsGroups {get; }
        private IValidatableChangeTrackingCollection<ProjectUnit> ProjectUnits { get; }

        public bool IsValid => this.ProjectUnits.Any() && ProjectUnits.All(projectUnit => projectUnit.IsValid);
        public bool IsChanged => ProjectUnits.IsChanged;

        public ProjectUnitGroupsContainer(IEnumerable<SalesUnit> salesUnits)
        {
            ProjectUnits = new ValidatableChangeTrackingCollection<ProjectUnit>(salesUnits.Select(salesUnit => new ProjectUnit(salesUnit)));
            
            var g = this.ProjectUnits
                .GroupBy(projectUnit => projectUnit, new ProjectUnit.ProjectUnitComparer())
                .Select(projectUnits => new ProjectUnitGroup(projectUnits));
            this.SalesUnitsGroups = new ObservableCollection<ProjectUnitGroup>(g);

            this.SalesUnitsGroups.CollectionChanged += (sender, args) =>
            {
                this.CollectionChanged?.Invoke(sender, args);
            };
        }

        public IEnumerator<ProjectUnitGroup> GetEnumerator()
        {
            return this.SalesUnitsGroups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(ProjectUnit projectUnit)
        {
            if (this.SalesUnitsGroups.Any(projectUnitGroup => projectUnitGroup.Add(projectUnit)) == false)
                this.SalesUnitsGroups.Add(new ProjectUnitGroup(new [] { projectUnit }));

            this.ProjectUnits.Add(projectUnit);
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}