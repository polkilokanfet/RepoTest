using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels.Groups
{
    public class SalesUnitOrderGroupsCollection : ObservableCollection<SalesUnitOrderGroup>
    {
        private object _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (Equals(_selectedItem, value)) return;
                _selectedItem = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedItem)));
                SelectedGroupChanged?.Invoke(_selectedItem);
            }
        }

        public SalesUnitOrderGroup SelectedGroup => SelectedItem as SalesUnitOrderGroup;

        public SalesUnitOrderItem SelectedUnit => SelectedItem as SalesUnitOrderItem;

        public bool IsGroupSelected => SelectedGroup != null;

        public bool IsUnitSelected => SelectedUnit != null;


        public event Action<object> SelectedGroupChanged;
    }
}