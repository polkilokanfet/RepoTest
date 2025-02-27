using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public class ProjectUnitGroupsContainer : ValidatableChangeTrackingCollection<ProjectUnit>
    {
        private IProjectUnit _selectedUnit;

        public IProjectUnit SelectedUnit
        {
            get => _selectedUnit;
            set
            {
                if (_selectedUnit == value) return;
                _selectedUnit = value;
                SelectedUnitChanged?.Invoke();
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedUnit)));
            }
        }

        public event Action SelectedUnitChanged;

        public IEnumerable<ProjectUnitGroup> Groups { get; }

        public ProjectUnitProductIncludedGroup SelectedProductsIncludedGroup { get; }

        public ProjectUnitGroupsContainer(IEnumerable<SalesUnit> salesUnits) : base(salesUnits.Select(salesUnit => new ProjectUnit(salesUnit)))
        {
            var groups = this
                .GroupBy(projectUnit => projectUnit, new ProjectUnit.ProjectUnitComparer())
                .Select(projectUnits => new ProjectUnitGroup(projectUnits));
            this.Groups = new ObservableCollection<ProjectUnitGroup>(groups);
        }

        public new void Add(ProjectUnit projectUnit)
        {
            if (this.Groups.Any(projectUnitGroup => projectUnitGroup.Add(projectUnit)) == false)
                ((ObservableCollection<ProjectUnitGroup>)this.Groups).Add(new ProjectUnitGroup(new [] { projectUnit }));

            base.Add(projectUnit);
        }
    }
}