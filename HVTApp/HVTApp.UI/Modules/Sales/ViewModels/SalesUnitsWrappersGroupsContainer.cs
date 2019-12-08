using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HVTApp.UI.Groups;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class SalesUnitsWrappersGroupsContainer : ObservableCollection<SalesUnitsWrappersGroup>
    {
        private SalesUnitsWrappersGroup _selectedGroup;

        public event Action<SalesUnitsWrappersGroup> SelectedGroupChanged;

        public SalesUnitsWrappersGroup SelectedGroup
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

        public void ClearAndAddRange(IEnumerable<SalesUnitsWrappersGroup> units)
        {
            this.SelectedGroup = null;
            this.Clear();
            this.AddRange(units);
        }
    }
}