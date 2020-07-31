using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HVTApp.Model.Wrapper.Groups;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitsWrappersGroupsContainer : ObservableCollection<ProjectUnitsGroup>
    {
        private ProjectUnitsGroup _selectedGroup;

        public event Action<ProjectUnitsGroup> SelectedGroupChanged;

        public ProjectUnitsGroup SelectedGroup
        {
            get { return _selectedGroup; }
            set
            {
                if (Equals(_selectedGroup, value)) return;
                _selectedGroup = value;
                SelectedGroupChanged?.Invoke(SelectedGroup);
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedGroup)));
            }
        }

        public void ClearAndAddRange(IEnumerable<ProjectUnitsGroup> units)
        {
            this.SelectedGroup = null;
            this.Clear();
            this.AddRange(units);
        }
    }
}